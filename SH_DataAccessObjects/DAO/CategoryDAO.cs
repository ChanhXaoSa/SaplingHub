using Microsoft.EntityFrameworkCore;
using SH_BusinessObjects.Common.Interface;
using SH_BusinessObjects.Entities;
using SH_DataAccessObjects.DAO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_DataAccessObjects.DAO
{
    public class CategoryDAO(IApplicationDbContext context) : ICategoryDAO
    {
        private readonly IApplicationDbContext _context = context;

        public async Task AddAsync(Category category)
        {
            await _context.Get<Category>().AddAsync(category);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await GetByIdAsync(id);
            if (category != null)
            {
                category.IsDeleted = true;
                _context.Get<Category>().Update(category);
                await _context.SaveChangesAsync(CancellationToken.None);
            }
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Get<Category>().Include(s => s.Saplings).ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _context.Get<Category>().Include(s => s.Saplings).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Get<Category>().Update(category);
            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }
}
