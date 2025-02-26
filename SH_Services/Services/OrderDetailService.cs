using SH_BusinessObjects.Common.Model.OrderDetail;
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
    public class OrderDetailService(IOrderDetailRepository OrderDetailRepository) : IOrderDetailService
    {
        private readonly IOrderDetailRepository _OrderDetailRepository = OrderDetailRepository;

        public async Task<List<OrderDetail>> GetAllAsync()
        {
            return await _OrderDetailRepository.GetAllAsync();
        }

        public async Task<OrderDetail?> GetByIdAsync(Guid id)
        {
            return await _OrderDetailRepository.GetByIdAsync(id);
        }

        public async Task<OrderDetail> CreateAsync(OrderDetailModel orderDetail)
        {
            OrderDetail newOrderDetail = new()
            {
                OrderId = orderDetail.OrderId,
                Quantity = orderDetail.Quantity,
                SaplingId = orderDetail.SaplingId,
                UnitPrice = orderDetail.UnitPrice,
            };
            await _OrderDetailRepository.AddAsync(newOrderDetail);
            return newOrderDetail;
        }

        public async Task UpdateAsync(Guid id, OrderDetailModel orderDetail)
        {
            var existingOrderDetail = await _OrderDetailRepository.GetByIdAsync(id);
            if (existingOrderDetail == null)
                throw new KeyNotFoundException("Không tìm thấy cây để cập nhật.");
            existingOrderDetail.OrderId = orderDetail.OrderId;
            existingOrderDetail.UnitPrice = orderDetail.UnitPrice;
            existingOrderDetail.SaplingId = id;
            existingOrderDetail.Quantity = orderDetail.Quantity;

            await _OrderDetailRepository.UpdateAsync(existingOrderDetail);
        }

        public async Task DeleteAsync(Guid id)
        {
            var orderDetail = await _OrderDetailRepository.GetByIdAsync(id);
            if (orderDetail == null)
                throw new KeyNotFoundException("Không tìm thấy cây để xóa.");

            await _OrderDetailRepository.DeleteAsync(id);
        }
    }
}
