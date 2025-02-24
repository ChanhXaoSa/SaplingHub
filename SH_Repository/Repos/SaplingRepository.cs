using SH_BusinessObjects.Common.Model.Sapling;
using SH_BusinessObjects.Entities;
using SH_DataAccessObjects.Context;
using SH_DataAccessObjects.DAO.Interfaces;
using SH_Repositories.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_Repositories.Repos
{
    public class SaplingRepository : ISaplingRepository
    {
        private readonly ISaplingDAO _saplingDAO;

        public SaplingRepository(ISaplingDAO saplingDAO)
        {
            _saplingDAO = saplingDAO;
        }

        public async Task<List<Sapling>> GetAllAsync()
        {
            return await _saplingDAO.GetAllAsync();
        }

        public async Task<Sapling?> GetByIdAsync(Guid id)
        {
            return await _saplingDAO.GetByIdAsync(id);
        }

        public async Task AddAsync(AddSaplingModel sapling)
        {
            await _saplingDAO.AddAsync(sapling);
        }

        public async Task UpdateAsync(Sapling sapling)
        {
            await _saplingDAO.UpdateAsync(sapling);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _saplingDAO.DeleteAsync(id);
        }
    }
}
