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
    public class OrderDetailRepository(IOrderDetailDAO orderDetailDAO) : IOrderDetailRepository
    {
        private readonly IOrderDetailDAO _orderDetailDAO = orderDetailDAO;

        public async Task<List<OrderDetail>> GetAllAsync()
        {
            return await _orderDetailDAO.GetAllAsync();
        }

        public async Task<OrderDetail?> GetByIdAsync(Guid id)
        {
            return await _orderDetailDAO.GetByIdAsync(id);
        }

        public async Task AddAsync(OrderDetail orderDetail)
        {
            await _orderDetailDAO.AddAsync(orderDetail);
        }

        public async Task UpdateAsync(OrderDetail orderDetail)
        {
            await _orderDetailDAO.UpdateAsync(orderDetail);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _orderDetailDAO.DeleteAsync(id);
        }
    }
}
