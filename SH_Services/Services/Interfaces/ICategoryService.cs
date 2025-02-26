using SH_BusinessObjects.Common.Model.Category;
using SH_BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_Services.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(Guid id);
        Task<Category> CreateAsync(CategoryModel category);
        Task UpdateAsync(Guid id, CategoryModel category);
        Task DeleteAsync(Guid id);
    }
}
