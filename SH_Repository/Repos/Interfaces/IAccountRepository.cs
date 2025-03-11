using Microsoft.AspNetCore.Identity;
using SH_BusinessObjects.Common.Model.Account;
using SH_BusinessObjects.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_Repositories.Repos.Interfaces
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(SignUpModel signUpModel);
        Task<object> SignInAsync(SignInModel signInModel);
        Task<ApplicationUser> GetAccountByIdAsync(string id);
        Task<List<ApplicationUser>> GetAllAsync();
        Task<List<ApplicationUser>> GetAdminAcocuntAsync();
        Task<List<ApplicationUser>> GetUserAccountAsync();
        Task<bool> LockoutEnableAccount(string email);
        Task<bool> LockoutDisableAccount(string email);
    }
}
