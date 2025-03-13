using Microsoft.EntityFrameworkCore;
using SH_BusinessObjects.Common.Interface;
using SH_BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_DataAccessObjects.DAO
{
    public class CartDAO(IApplicationDbContext context)
    {
        private readonly IApplicationDbContext _context = context;

        public async Task<List<Cart>> GetAllAsync()
        {
            return await _context.Get<Cart>().ToListAsync();
        }

        public async Task<Cart?> GetByIdAsync(Guid id)
        {
            return await _context.Get<Cart>().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(Cart cart)
        {
            await _context.Get<Cart>().AddAsync(cart);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task UpdateAsync(Cart cart)
        {
            _context.Get<Cart>().Update(cart);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task DeleteAsync(Guid id)
        {
            var cart = await GetByIdAsync(id);
            if (cart != null)
            {
                _context.Get<Cart>().Remove(cart);
                await _context.SaveChangesAsync(CancellationToken.None);
            }
        }
    }
}
