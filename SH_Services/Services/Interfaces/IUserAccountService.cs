﻿using SH_BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_Services.Services.Interfaces
{
    public interface IUserAccountService
    {
        Task<List<UserAccount>> GetAllAsync();
        Task<UserAccount?> GetByIdAsync(Guid id);
        Task AddAsync(UserAccount userAccount);
        Task UpdateAsync(UserAccount userAccount);
        Task DeleteAsync(Guid id);
    }
}
