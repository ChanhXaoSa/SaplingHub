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
    public class UserAccountService(IUserAccountRepository userAccountRepository) : IUserAccountService
    {
        private readonly IUserAccountRepository _userAccountRepository = userAccountRepository;

        public async Task AddAsync(UserAccount userAccount)
        {
            await _userAccountRepository.AddAsync(userAccount);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _userAccountRepository.DeleteAsync(id);
        }

        public async Task<List<UserAccount>> GetAllAsync()
        {
            return await _userAccountRepository.GetAllAsync();
        }

        public async Task<UserAccount?> GetByIdAsync(Guid id)
        {
            return await _userAccountRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(UserAccount userAccount)
        {
            await _userAccountRepository.UpdateAsync(userAccount);
        }
    }
}
