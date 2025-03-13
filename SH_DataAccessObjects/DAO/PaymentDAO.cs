using Microsoft.EntityFrameworkCore;
using SH_BusinessObjects.Common.Interface;
using SH_BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_DataAccessObjects.DAO
{
    public class PaymentDAO(IApplicationDbContext context)
    {
        private readonly IApplicationDbContext _context = context;

        public async Task<List<Payment>> GetAllAsync()
        {
            return await _context.Get<Payment>().ToListAsync();
        }

        public async Task<Payment?> GetByIdAsync(Guid id)
        {
            return await _context.Get<Payment>().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(Payment payment)
        {
            await _context.Get<Payment>().AddAsync(payment);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task UpdateAsync(Payment payment)
        {
            _context.Get<Payment>().Update(payment);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task DeleteAsync(Guid id)
        {
            var payment = await GetByIdAsync(id);
            if (payment != null)
            {
                payment.IsDeleted = true;
                _context.Get<Payment>().Update(payment);
                await _context.SaveChangesAsync(CancellationToken.None);
            }
        }
    }
}
