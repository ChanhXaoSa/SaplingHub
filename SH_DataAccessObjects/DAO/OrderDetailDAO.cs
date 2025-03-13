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
    public class OrderDetailDAO(IApplicationDbContext context)
    {
        private readonly IApplicationDbContext _context = context;

        public async Task<List<OrderDetail>> GetAllAsync()
        {
            return await _context.Get<OrderDetail>().ToListAsync();
        }

        public async Task<OrderDetail?> GetByIdAsync(Guid id)
        {
            return await _context.Get<OrderDetail>().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(OrderDetail orderDetail)
        {
            await _context.Get<OrderDetail>().AddAsync(orderDetail);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task UpdateAsync(OrderDetail orderDetail)
        {
            _context.Get<OrderDetail>().Update(orderDetail);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task DeleteAsync(Guid id)
        {
            var orderDetail = await GetByIdAsync(id);
            if (orderDetail != null)
            {
                orderDetail.IsDeleted = true;
                _context.Get<OrderDetail>().Update(orderDetail);
                await _context.SaveChangesAsync(CancellationToken.None);
            }
        }

        public async Task<List<OrderDetail>> GetByOrderIdAsync(Guid orderId)
        {
            return await _context.Get<OrderDetail>().Where(s => s.OrderId == orderId).ToListAsync();
        }

        public async Task<bool> OrderExistsAsync(Guid orderId)
        {
            return await _context.Get<Order>().AnyAsync(s => s.Id == orderId);
        }

        public async Task<bool> SaplingExistsAsync(Guid saplingId)
        {
            return await _context.Get<Sapling>().AnyAsync(s => s.Id == saplingId);
        }
    }
}
