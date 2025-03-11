using Microsoft.AspNetCore.Identity;
using SH_BusinessObjects.Common.Model.Account;
using SH_BusinessObjects.Identity;
using SH_Repositories.Repos.Interfaces;
using SH_Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_Services.Services
{
    public class AccountService(IAccountRepository accountRepository) : IAccountService
    {
        private readonly IAccountRepository _accountRepository = accountRepository;
        public async Task<ApplicationUser> GetAccountByIdAsync(string id)
        {
            return await _accountRepository.GetAccountByIdAsync(id);
        }

        public async Task<List<ApplicationUser>> GetAdminAcocuntAsync()
        {
            return await _accountRepository.GetAdminAcocuntAsync();
        }

        public async Task<List<ApplicationUser>> GetAllAsync()
        {
            return await _accountRepository.GetAllAsync();
        }

        public async Task<List<ApplicationUser>> GetUserAccountAsync()
        {
            return await _accountRepository.GetUserAccountAsync();
        }

        public async Task<bool> LockoutDisableAccount(string email)
        {
            return await _accountRepository.LockoutDisableAccount(email);
        }

        public async Task<bool> LockoutEnableAccount(string email)
        {
            return await _accountRepository.LockoutEnableAccount(email);
        }

        public async Task<object> SignInAsync(SignInModel signInModel)
        {
            return await _accountRepository.SignInAsync(signInModel);
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel signUpModel)
        {
            return await _accountRepository.SignUpAsync(signUpModel);
        }
    }
}
