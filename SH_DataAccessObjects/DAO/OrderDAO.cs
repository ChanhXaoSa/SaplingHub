using Microsoft.EntityFrameworkCore;
using SH_BusinessObjects.Common.Interface;
using SH_BusinessObjects.Entities;
using SH_BusinessObjects.Identity;
using SH_DataAccessObjects.DAO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_DataAccessObjects.DAO
{
    public class OrderDAO(IApplicationDbContext context, IOrderDetailDAO orderDetailDAO, ICartDAO cartDAO)
    {
        private readonly IApplicationDbContext _context = context;

        private readonly IOrderDetailDAO _orderDetailDAO = orderDetailDAO;

        private readonly ICartDAO _cartDAO = cartDAO;

        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Get<Order>().ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _context.Get<Order>().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(Order order)
        {
            await _context.Get<Order>().AddAsync(order);
            var cart = await _cartDAO.GetByUserIdAsync(order.UserId);
            if (cart != null)
            {
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail
                    {
                        SaplingId = item.SaplingId,
                        Quantity = item.Quantity,
                        OrderId = order.Id,
                        UnitPrice = item.Quantity * item.Sapling!.Price,
                    };
                    await _orderDetailDAO.AddAsync(orderDetail);
                }
            }
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Get<Order>().Update(order);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task DeleteAsync(Guid id)
        {
            var order = await GetByIdAsync(id);
            if (order != null)
            {
                order.IsDeleted = true;
                _context.Get<Order>().Update(order);
                await _context.SaveChangesAsync(CancellationToken.None);
            }
        }

        public async Task<List<Order>> GetByUserIdAsync(string userId)
        {
            return await _context.Get<Order>().Where(s => s.UserId == userId).ToListAsync();
        }

        public async Task<bool> UserExistsAsync(string userId)
        {
            return await _context.GetUser<ApplicationUser>().AnyAsync(s => s.Id == userId);
        }
    }
}
