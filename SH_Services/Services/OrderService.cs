using SH_BusinessObjects.Common.Model.Order;
using SH_BusinessObjects.Entities;
using SH_Repositories.Repos.Interfaces;
using SH_Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_Services.Services
{
    public class OrderService(IOrderRepository OrderRepository) : IOrderService
    {
        private readonly IOrderRepository _OrderRepository = OrderRepository;

        public async Task<List<Order>> GetAllAsync()
        {
            return await _OrderRepository.GetAllAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _OrderRepository.GetByIdAsync(id);
        }

        public async Task<Order> CreateAsync(OrderModel order)
        {
            Order newOrder = new()
            {
                UserId = order.UserId,
                ShippingAddress = order.ShippingAddress,
                OrderDate = order.OrderDate,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
            };
            await _OrderRepository.AddAsync(newOrder);
            return newOrder;
        }

        public async Task UpdateAsync(Guid id, OrderModel order)
        {
            var existingOrder = await _OrderRepository.GetByIdAsync(id);
            if (existingOrder == null)
                throw new KeyNotFoundException("Không tìm thấy cây để cập nhật.");

            existingOrder.OrderDate = order.OrderDate;
            existingOrder.Status = order.Status;
            existingOrder.ShippingAddress = order.ShippingAddress;
            existingOrder.TotalAmount = order.TotalAmount;

            await _OrderRepository.UpdateAsync(existingOrder);
        }

        public async Task DeleteAsync(Guid id)
        {
            var order = await _OrderRepository.GetByIdAsync(id);
            if (order == null)
                throw new KeyNotFoundException("Không tìm thấy cây để xóa.");

            await _OrderRepository.DeleteAsync(id);
        }
    }
}
