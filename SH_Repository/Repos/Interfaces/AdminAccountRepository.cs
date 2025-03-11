using SH_BusinessObjects.Entities;
using SH_DataAccessObjects.DAO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_Repositories.Repos.Interfaces
{
    public class AdminAccountRepository(IAdminAccountDAO adminAccountDAO) : IAdminAccountRepository
    {
        private readonly IAdminAccountDAO _adminAccountDAO = adminAccountDAO;

        public async Task AddAsync(AdminAccount adminAccount)
        {
            await _adminAccountDAO.AddAsync(adminAccount);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _adminAccountDAO.DeleteAsync(id);
        }

        public async Task<List<AdminAccount>> GetAllAsync()
        {
            return await _adminAccountDAO.GetAllAsync();
        }

        public async Task<AdminAccount?> GetByIdAsync(Guid id)
        {
            return await _adminAccountDAO.GetByIdAsync(id);
        }

        public async Task UpdateAsync(AdminAccount adminAccount)
        {
            await _adminAccountDAO.UpdateAsync(adminAccount);
        }
    }
}
