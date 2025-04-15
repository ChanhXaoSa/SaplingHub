using SH_BusinessObjects.Entities;
using SH_DataAccessObjects.DAO.Interfaces;
using SH_Repositories.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_Repositories.Repos
{
    public class AuctionBidRepository(IAuctionBidDAO auctionBidDAO) : IAuctionBidRepository
    {
        private readonly IAuctionBidDAO _auctionBidDAO = auctionBidDAO;
        public async Task<List<AuctionBid>> GetAllAsync()
        {
            return await _auctionBidDAO.GetAllAsync();
        }
        public async Task<AuctionBid?> GetByIdAsync(Guid id)
        {
            return await _auctionBidDAO.GetByIdAsync(id);
        }
        public async Task AddAsync(AuctionBid auctionBid)
        {
            await _auctionBidDAO.AddAsync(auctionBid);
        }
        public async Task UpdateAsync(AuctionBid auctionBid)
        {
            await _auctionBidDAO.UpdateAsync(auctionBid);
        }
        public async Task DeleteAsync(Guid id)
        {
            await _auctionBidDAO.DeleteAsync(id);
        }
    }
}
