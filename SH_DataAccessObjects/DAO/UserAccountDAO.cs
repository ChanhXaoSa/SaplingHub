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
    public class UserAccountDAO(IApplicationDbContext context) : IUserAccountDAO
    {
        private readonly IApplicationDbContext _context = context;
        public async Task AddAsync(UserAccount userAccount)
        {
            await _context.Get<UserAccount>().AddAsync(userAccount);
            await _context.SaveChangesAsync(CancellationToken.None);
        }
        public async Task DeleteAsync(Guid id)
        {
            var userAccount = await GetByIdAsync(id);
            if (userAccount != null)
            {
                userAccount.IsDeleted = true;
                _context.Get<UserAccount>().Update(userAccount);
                await _context.SaveChangesAsync(CancellationToken.None);
            }
        }
        public async Task<List<UserAccount>> GetAllAsync()
        {
            return await _context.Get<UserAccount>().ToListAsync();
        }
        public async Task<UserAccount?> GetByIdAsync(Guid id)
        {
            return await _context.Get<UserAccount>().FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task UpdateAsync(UserAccount userAccount)
        {
            _context.Get<UserAccount>().Update(userAccount);
            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }
}
