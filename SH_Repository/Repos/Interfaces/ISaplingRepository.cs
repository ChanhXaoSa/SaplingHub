using SH_BusinessObjects.Common.Model.Sapling;
using SH_BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_Repositories.Repos.Interfaces
{
    public interface ISaplingRepository
    {
        Task<List<Sapling>> GetAllAsync();
        Task<Sapling?> GetByIdAsync(Guid id);
        Task AddAsync(Sapling sapling);
        Task UpdateAsync(Sapling sapling);
        Task DeleteAsync(Guid id);
    }
}
