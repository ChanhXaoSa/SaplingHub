using SH_BusinessObjects.Entities;
using SH_DataAccessObjects.DAO;
using SH_Repositories.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_Repositories.Repos
{
    public class UserAccountRepository(UserAccountDAO userAccountDAO) : IUserAccountRepository
    {
        private readonly UserAccountDAO _userAccountDAO = userAccountDAO;

        public async Task AddAsync(UserAccount userAccount)
        {
            await _userAccountDAO.AddAsync(userAccount);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _userAccountDAO.DeleteAsync(id);
        }

        public async Task<List<UserAccount>> GetAllAsync()
        {
            return await _userAccountDAO.GetAllAsync();
        }

        public async Task<UserAccount?> GetByIdAsync(Guid id)
        {
            return await _userAccountDAO.GetByIdAsync(id);
        }

        public async Task UpdateAsync(UserAccount userAccount)
        {
            await _userAccountDAO.UpdateAsync(userAccount);
        }
    }
}
