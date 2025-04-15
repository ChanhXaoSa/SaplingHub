using SH_BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_DataAccessObjects.DAO.Interfaces
{
    public interface IAuctionBidDAO
    {
        Task<List<AuctionBid>> GetAllAsync();
        Task<AuctionBid?> GetByIdAsync(Guid id);
        Task AddAsync(AuctionBid auctionBid);
        Task UpdateAsync(AuctionBid auctionBid);
        Task DeleteAsync(Guid id);
    }
}
