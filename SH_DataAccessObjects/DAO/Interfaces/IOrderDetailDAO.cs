using SH_BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_DataAccessObjects.DAO.Interfaces
{
    public interface IOrderDetailDAO
    {
        Task<List<OrderDetail>> GetAllAsync();
        Task<OrderDetail?> GetByIdAsync(Guid id);
        Task AddAsync(OrderDetail orderDetail);
        Task UpdateAsync(OrderDetail orderDetail);
        Task DeleteAsync(Guid id);
        Task<List<OrderDetail>> GetByOrderIdAsync(Guid orderId);
        Task<bool> OrderExistsAsync(Guid orderId);
        Task<bool> SaplingExistsAsync(Guid saplingId);
    }
}
