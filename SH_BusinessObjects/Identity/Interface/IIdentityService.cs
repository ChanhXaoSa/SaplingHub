﻿using SH_BusinessObjects.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_BusinessObjects.Identity.Interface
{
    public interface IIdentityService
    {
        Task<string?> GetUserNameAsync(string userId);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<bool> AuthorizeAsync(string userId, string policyName);

        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

        Task<Result> DeleteUserAsync(string userId);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
    }
}
