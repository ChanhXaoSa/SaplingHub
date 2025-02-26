using SH_BusinessObjects.Common.Model.Sapling;
using SH_BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_Services.Services.Interfaces
{
    public interface ISaplingService
    {
        Task<List<Sapling>> GetAllAsync();
        Task<Sapling?> GetByIdAsync(Guid id);
        Task<Sapling> CreateAsync(SaplingModel sapling);
        Task UpdateAsync(Guid id, SaplingModel sapling);
        Task DeleteAsync(Guid id);
    }
}
