﻿using SH_BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_Services.Services.Interfaces
{
    public interface IAdminAccountService
    {
        Task<List<AdminAccount>> GetAllAsync();
        Task<AdminAccount?> GetByIdAsync(Guid id);
        Task AddAsync(AdminAccount adminAccount);
        Task UpdateAsync(AdminAccount adminAccount);
        Task DeleteAsync(Guid id);
    }
}
