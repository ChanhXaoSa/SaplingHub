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
    public class SaplingDAO : ISaplingDAO
    {
        private readonly IApplicationDbContext _context;

        public SaplingDAO(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Sapling>> GetAllAsync()
        {
            return await _context.Get<Sapling>().Include(s => s.Category).ToListAsync();
        }

        public async Task<Sapling?> GetByIdAsync(Guid id)
        {
            return await _context.Get<Sapling>().Include(s => s.Category).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(AddSaplingModel sapling)
        {
            Sapling item = new Sapling()
            {
                Id = new Guid(),
                Name = sapling.Name,
                Price = sapling.Price,
                StockQuantity = sapling.StockQuantity,
                CategoryId = sapling.CategoryId,
                ImageUrl = sapling.ImageUrl,
            };
            await _context.Get<Sapling>().AddAsync(item);
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
                _context.Get<Sapling>().Remove(sapling);
                await _context.SaveChangesAsync(CancellationToken.None);
            }
        }
    }
}
