using Microsoft.EntityFrameworkCore;
using SH_BusinessObjects.Common.Interface;
using SH_BusinessObjects.Entities;
using SH_DataAccessObjects.DAO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_DataAccessObjects.DAO
{
    public class AdminAccountDAO(IApplicationDbContext context) : IAdminAccountDAO
    {
        private readonly IApplicationDbContext _context = context;

        public async Task<List<AdminAccount>> GetAllAsync()
        {
            return await _context.Get<AdminAccount>().ToListAsync();
        }

        public async Task<AdminAccount?> GetByIdAsync(Guid id)
        {
            return await _context.Get<AdminAccount>().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(AdminAccount adminAccount)
        {
            await _context.Get<AdminAccount>().AddAsync(adminAccount);
            await _context.SaveChangesAsync(CancellationToken.None);    
        }

        public async Task UpdateAsync(AdminAccount adminAccount)
        {
            _context.Get<AdminAccount>().Update(adminAccount);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task DeleteAsync(Guid id)
        {
            var adminAccount = await GetByIdAsync(id);
            if (adminAccount != null)
            {
                adminAccount.IsDeleted = true;
                _context.Get<AdminAccount>().Update(adminAccount);
                await _context.SaveChangesAsync(CancellationToken.None);
            }
        }
    }
}
