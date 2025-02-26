using SH_BusinessObjects.Common.Model.Payment;
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
    public class PaymentService(IPaymentRepository PaymentRepository) : IPaymentService
    {
        private readonly IPaymentRepository _PaymentRepository = PaymentRepository;

        public async Task<List<Payment>> GetAllAsync()
        {
            return await _PaymentRepository.GetAllAsync();
        }

        public async Task<Payment?> GetByIdAsync(Guid id)
        {
            return await _PaymentRepository.GetByIdAsync(id);
        }

        public async Task<Payment> CreateAsync(PaymentModel payment)
        {
            Payment newPayment = new()
            {
                OrderId = payment.OrderId,
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                PaymentMethod = payment.PaymentMethod,
                PaymentStatus = payment.PaymentStatus,
            };
            await _PaymentRepository.AddAsync(newPayment);
            return newPayment;
        }

        public async Task UpdateAsync(Guid id, PaymentModel payment)
        {
            var existingPayment = await _PaymentRepository.GetByIdAsync(id);
            if (existingPayment == null)
                throw new KeyNotFoundException("Không tìm thấy cây để cập nhật.");

            existingPayment.Amount = payment.Amount;
            existingPayment.PaymentDate = payment.PaymentDate;
            existingPayment.OrderId = payment.OrderId;
            existingPayment.PaymentStatus = payment.PaymentStatus;
            existingPayment.PaymentMethod = payment.PaymentMethod;

            await _PaymentRepository.UpdateAsync(existingPayment);
        }

        public async Task DeleteAsync(Guid id)
        {
            var payment = await _PaymentRepository.GetByIdAsync(id);
            if (payment == null)
                throw new KeyNotFoundException("Không tìm thấy cây để xóa.");

            await _PaymentRepository.DeleteAsync(id);
        }
    }
}
