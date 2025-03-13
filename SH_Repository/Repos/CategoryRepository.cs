using SH_BusinessObjects.Entities;
using SH_DataAccessObjects.DAO;
using SH_Repositories.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_Repositories.Repos
{
    public class CategoryRepository(CategoryDAO categoryDAO) : ICategoryRepository
    {
        private readonly CategoryDAO _categoryDAO = categoryDAO;

        public async Task<List<Category>> GetAllAsync()
        {
            return await _categoryDAO.GetAllAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _categoryDAO.GetByIdAsync(id);
        }

        public async Task AddAsync(Category category)
        {
            await _categoryDAO.AddAsync(category);
        }

        public async Task UpdateAsync(Category category)
        {
            await _categoryDAO.UpdateAsync(category);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _categoryDAO.DeleteAsync(id);
        }
    }
}
