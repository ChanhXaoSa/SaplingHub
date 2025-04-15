using SH_BusinessObjects.Entities;
using SH_DataAccessObjects.DAO;
using SH_DataAccessObjects.DAO.Interfaces;
using SH_Repositories.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_Repositories.Repos
{
    public class CartRepository(ICartDAO cartDAO) : ICartRepository
    {
        private readonly ICartDAO _cartDAO = cartDAO;

        public async Task<List<Cart>> GetAllAsync()
        {
            return await _cartDAO.GetAllAsync();
        }

        public async Task<Cart?> GetByIdAsync(Guid id)
        {
            return await _cartDAO.GetByIdAsync(id);
        }

        public async Task AddAsync(Cart cart)
        {
            await _cartDAO.AddAsync(cart);
        }

        public async Task UpdateAsync(Cart cart)
        {
            await _cartDAO.UpdateAsync(cart);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _cartDAO.DeleteAsync(id);
        }
    }
}
