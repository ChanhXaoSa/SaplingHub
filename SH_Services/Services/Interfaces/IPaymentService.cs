using SH_BusinessObjects.Common.Model.Payment;
using SH_BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_Services.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<List<Payment>> GetAllAsync();
        Task<Payment?> GetByIdAsync(Guid id);
        Task<Payment> CreateAsync(PaymentModel payment);
        Task UpdateAsync(Guid id, PaymentModel payment);
        Task DeleteAsync(Guid id);
    }
}
