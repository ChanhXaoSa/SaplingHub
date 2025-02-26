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
    public class OrderRepository(IOrderDAO orderDAO) : IOrderRepository
    {
        private readonly IOrderDAO _orderDAO = orderDAO;

        public async Task<List<Order>> GetAllAsync()
        {
            return await _orderDAO.GetAllAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _orderDAO.GetByIdAsync(id);
        }

        public async Task AddAsync(Order order)
        {
            await _orderDAO.AddAsync(order);
        }

        public async Task UpdateAsync(Order order)
        {
            await _orderDAO.UpdateAsync(order);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _orderDAO.DeleteAsync(id);
        }
    }
}
