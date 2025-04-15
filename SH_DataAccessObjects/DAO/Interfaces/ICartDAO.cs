using SH_BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_DataAccessObjects.DAO.Interfaces
{
    public interface ICartDAO
    {
        Task<List<Cart>> GetAllAsync();
        Task<Cart?> GetByIdAsync(Guid id);
        Task AddAsync(Cart cart);
        Task UpdateAsync(Cart cart);
        Task DeleteAsync(Guid id);
        Task<List<Cart>> GetByUserIdAsync(string userId);
        Task<bool> UserExistsAsync(string userId);
        Task<bool> SaplingExistsAsync(Guid saplingId);
    }
}
