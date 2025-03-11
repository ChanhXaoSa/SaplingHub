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
    public class AdminAccountService(IAdminAccountRepository adminAccountRepository) : IAdminAccountService
    {
        private readonly IAdminAccountRepository _adminAccountRepository = adminAccountRepository;

        public async Task AddAsync(AdminAccount adminAccount)
        {
            await _adminAccountRepository.AddAsync(adminAccount);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _adminAccountRepository.DeleteAsync(id);
        }

        public async Task<List<AdminAccount>> GetAllAsync()
        {
            return await _adminAccountRepository.GetAllAsync();
        }

        public Task<AdminAccount?> GetByIdAsync(Guid id)
        {
            return _adminAccountRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(AdminAccount adminAccount)
        {
            await _adminAccountRepository.UpdateAsync(adminAccount);
        }
    }
}
