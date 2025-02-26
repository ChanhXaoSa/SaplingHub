using SH_BusinessObjects.Common.Model.Category;
using SH_BusinessObjects.Entities;
using SH_Repositories.Repos.Interfaces;
using SH_Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_Services.Services
{
    public class CategoryService(ICategoryRepository CategoryRepository) : ICategoryService
    {
        private readonly ICategoryRepository _CategoryRepository = CategoryRepository;

        public async Task<List<Category>> GetAllAsync()
        {
            return await _CategoryRepository.GetAllAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _CategoryRepository.GetByIdAsync(id);
        }

        public async Task<Category> CreateAsync(CategoryModel category)
        {
            Category newCategory = new()
            {
                Name = category.Name,
                Description = category.Description,
            };
            await _CategoryRepository.AddAsync(newCategory);
            return newCategory;
        }

        public async Task UpdateAsync(Guid id, CategoryModel category)
        {
            var existingCategory = await _CategoryRepository.GetByIdAsync(id);
            if (existingCategory == null)
                throw new KeyNotFoundException("Không tìm thấy cây để cập nhật.");

            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;

            await _CategoryRepository.UpdateAsync(existingCategory);
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await _CategoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new KeyNotFoundException("Không tìm thấy cây để xóa.");

            await _CategoryRepository.DeleteAsync(id);
        }
    }
}
