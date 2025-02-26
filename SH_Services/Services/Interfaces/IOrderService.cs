using SH_BusinessObjects.Common.Model.Order;
using SH_BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_Services.Services.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(Guid id);
        Task<Order> CreateAsync(OrderModel order);
        Task UpdateAsync(Guid id, OrderModel order);
        Task DeleteAsync(Guid id);
    }
}
