using Microsoft.EntityFrameworkCore;
using SH_BusinessObjects.Common.Interface;
using SH_BusinessObjects.Entities;
using SH_DataAccessObjects.DAO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_DataAccessObjects.DAO
{
    public class AuctionPlantDAO(IApplicationDbContext context) : IAuctionPlantDAO
    {
        private readonly IApplicationDbContext _context = context;
        public async Task<List<AuctionPlant>> GetAllAsync()
        {
            return await _context.Get<AuctionPlant>().ToListAsync();
        }
        public async Task<AuctionPlant?> GetByIdAsync(Guid id)
        {
            return await _context.Get<AuctionPlant>()
                .Include(a => a.AuctionBids)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task AddAsync(AuctionPlant auctionPlant)
        {
            await _context.Get<AuctionPlant>().AddAsync(auctionPlant);
            await _context.SaveChangesAsync(CancellationToken.None);
        }
        public async Task UpdateAsync(AuctionPlant auctionPlant)
        {
            _context.Get<AuctionPlant>().Update(auctionPlant);
            await _context.SaveChangesAsync(CancellationToken.None);
        }
        public async Task UpdateCurrentHighestBidAsync(Guid auctionPlantId, decimal currentHighestBid)
        {
            var auctionPlant = await GetByIdAsync(auctionPlantId);
            if (auctionPlant != null)
            {
                auctionPlant.CurrentHighestBid = currentHighestBid;
                _context.Get<AuctionPlant>().Update(auctionPlant);
            }
            if(auctionPlant?.AuctionBids != null)
            {
                foreach (var bid in auctionPlant.AuctionBids)
                {
                    if (bid.BidAmount == currentHighestBid)
                    {
                        bid.IsWinningBid = true;
                    }
                    else
                    {
                        bid.IsWinningBid = false;
                    }
                }
            }
            await _context.SaveChangesAsync(CancellationToken.None);
        }
        public async Task DeleteAsync(Guid id)
        {
            var auctionPlant = await GetByIdAsync(id);
            if (auctionPlant != null)
            {
                auctionPlant.IsDeleted = true;
                _context.Get<AuctionPlant>().Update(auctionPlant);
                await _context.SaveChangesAsync(CancellationToken.None);
            }
        }
    }
}
