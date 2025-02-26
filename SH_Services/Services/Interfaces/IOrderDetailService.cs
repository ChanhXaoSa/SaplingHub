using SH_BusinessObjects.Common.Model.OrderDetail;
using SH_BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_Services.Services.Interfaces
{
    public interface IOrderDetailService
    {
        Task<List<OrderDetail>> GetAllAsync();
        Task<OrderDetail?> GetByIdAsync(Guid id);
        Task<OrderDetail> CreateAsync(OrderDetailModel orderDetail);
        Task UpdateAsync(Guid id, OrderDetailModel orderDetail);
        Task DeleteAsync(Guid id);
    }
}
