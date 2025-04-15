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
    public class AuctionBidDAO(IApplicationDbContext context) : IAuctionBidDAO
    {
        private readonly IApplicationDbContext _context = context;
        public async Task<List<AuctionBid>> GetAllAsync()
        {
            return await _context.Get<AuctionBid>().ToListAsync();
        }
        public async Task<AuctionBid?> GetByIdAsync(Guid id)
        {
            return await _context.Get<AuctionBid>().FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task AddAsync(AuctionBid auctionBid)
        {
            await _context.Get<AuctionBid>().AddAsync(auctionBid);
            await _context.SaveChangesAsync(CancellationToken.None);
        }
        public async Task UpdateAsync(AuctionBid auctionBid)
        {
            _context.Get<AuctionBid>().Update(auctionBid);
            await _context.SaveChangesAsync(CancellationToken.None);
        }
        public async Task DeleteAsync(Guid id)
        {
            var auctionBid = await GetByIdAsync(id);
            if (auctionBid != null)
            {
                auctionBid.IsDeleted = true;
                _context.Get<AuctionBid>().Update(auctionBid);
                await _context.SaveChangesAsync(CancellationToken.None);
            }
        }
    }
}
