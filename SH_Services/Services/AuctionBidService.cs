using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using SH_BusinessObjects.Entities;
using SH_Repositories.Repos.Interfaces;
using SH_Services.Services.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_Services.Services
{
    public class AuctionBidService : IAuctionBidService
    {
        private readonly RedLockFactory _redLockFactory;
        private readonly IAuctionBidRepository _auctionBidRepository;
        private readonly IAuctionPlantRepository _auctionPlantRepository;

        public AuctionBidService(ConnectionMultiplexer redis, IAuctionBidRepository repository, IAuctionPlantRepository auctionPlantRepository)
        {
            var multiplexers = new List<RedLockMultiplexer> { redis };
            _redLockFactory = RedLockFactory.Create(multiplexers);
            _auctionBidRepository = repository;
            _auctionPlantRepository = auctionPlantRepository;
        }

        public async Task<(bool Success, string Message)> PlaceBidAsync(Guid auctionPlantId, string userId, decimal bidAmount)
        {
            var auctionPlant = await _auctionPlantRepository.GetByIdAsync(auctionPlantId);
            if (bidAmount <= (auctionPlant?.CurrentHighestBid ?? 0))
            {
                return (false, "Bid amount must be greater than the current bid amount.");
            }

            var resource = $"auction_bid_{auctionPlantId}";
            var expiry = TimeSpan.FromSeconds(2);
            var waitTime = TimeSpan.FromSeconds(1);
            var retry = TimeSpan.FromMilliseconds(200);

            await using var redLock = await _redLockFactory.CreateLockAsync(resource, expiry, waitTime, retry);
            if (redLock.IsAcquired)
            {
                auctionPlant = await _auctionPlantRepository.GetByIdAsync(auctionPlantId);
                if(bidAmount <= (auctionPlant?.CurrentHighestBid ?? 0))
                {
                    return (false, "Bid amount must be greater than the current bid amount.");
                }

                var bid = new AuctionBid
                {
                    AuctionPlantId = auctionPlantId,
                    UserId = userId,
                    BidAmount = bidAmount,
                    BidTime = DateTime.UtcNow,
                    IsWinningBid = true,
                    BidderName = ""
                };

                await _auctionBidRepository.AddAsync(bid);
                await _auctionPlantRepository.UpdateCurrentHighestBidAsync(auctionPlantId, bidAmount);

                return (true, "Bid placed successfully.");
            }

            return (false, "Could not acquire lock to place bid.");
        }

        public void Dispose()
        {
            _redLockFactory.Dispose();
        }
    }
}
