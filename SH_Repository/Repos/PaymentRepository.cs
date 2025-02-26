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
    public class PaymentRepository(IPaymentDAO paymentDAO) : IPaymentRepository
    {
        private readonly IPaymentDAO _paymentDAO = paymentDAO;

        public async Task<List<Payment>> GetAllAsync()
        {
            return await _paymentDAO.GetAllAsync();
        }

        public async Task<Payment?> GetByIdAsync(Guid id)
        {
            return await _paymentDAO.GetByIdAsync(id);
        }

        public async Task AddAsync(Payment payment)
        {
            await _paymentDAO.AddAsync(payment);
        }

        public async Task UpdateAsync(Payment payment)
        {
            await _paymentDAO.UpdateAsync(payment);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _paymentDAO.DeleteAsync(id);
        }
    }
}
