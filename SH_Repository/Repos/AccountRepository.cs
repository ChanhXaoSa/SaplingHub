using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using SH_BusinessObjects.Common.Interface;
using SH_BusinessObjects.Common.Model.Account;
using SH_BusinessObjects.Enum;
using SH_BusinessObjects.Identity;
using SH_BusinessObjects.Identity.Interface;
using SH_DataAccessObjects.DAO.Interfaces;
using SH_Repositories.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SH_Repositories.Repos
{
    public class AccountRepository(IAccountDAO accountDAO) : IAccountRepository
    {
        private readonly IAccountDAO _accountDAO = accountDAO;

        public async Task<ApplicationUser> GetAccountByIdAsync(string id)
        {
            return await _accountDAO.GetAccountByIdAsync(id);
        }

        public async Task<List<ApplicationUser>> GetAdminAcocuntAsync()
        {
            return await _accountDAO.GetAdminAcocuntAsync();
        }

        public async Task<List<ApplicationUser>> GetAllAsync()
        {
            return await _accountDAO.GetAllAsync();
        }

        public async Task<List<ApplicationUser>> GetUserAccountAsync()
        {
            return await _accountDAO.GetUserAccountAsync();
        }

        public async Task<bool> LockoutDisableAccount(string email)
        {
            return await _accountDAO.LockoutDisableAccount(email);
        }

        public async Task<bool> LockoutEnableAccount(string email)
        {
            return await _accountDAO.LockoutEnableAccount(email);
        }

        public async Task<object> SignInAsync(SignInModel signInModel)
        {
            return await _accountDAO.SignInAsync(signInModel);
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel signUpModel)
        {
            return await _accountDAO.SignUpAsync(signUpModel);
        }
    }
}
