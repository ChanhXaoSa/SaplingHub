using Microsoft.EntityFrameworkCore;
using SH_BusinessObjects.Common.Interface;
using SH_BusinessObjects.Common.Model.Sapling;
using SH_BusinessObjects.Entities;
using SH_DataAccessObjects.Context;
using SH_DataAccessObjects.DAO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_DataAccessObjects.DAO
{
    public class SaplingDAO(IApplicationDbContext context) : ISaplingDAO
    {
        private readonly IApplicationDbContext _context = context;

        public async Task<List<Sapling>> GetAllAsync()
        {
            return await _context.Get<Sapling>().Include(s => s.Category).ToListAsync();
        }

        public async Task<Sapling?> GetByIdAsync(Guid id)
        {
            return await _context.Get<Sapling>().Include(s => s.Category).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(Sapling sapling)
        {
            await _context.Get<Sapling>().AddAsync(sapling);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task UpdateAsync(Sapling sapling)
        {
            _context.Get<Sapling>().Update(sapling);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task DeleteAsync(Guid id)
        {
            var sapling = await GetByIdAsync(id);
            if (sapling != null)
            {
                sapling.IsDeleted = true;
                _context.Get<Sapling>().Update(sapling);
                //_context.Get<Sapling>().Remove(sapling);
                await _context.SaveChangesAsync(CancellationToken.None);
            }
        }
    }
}
