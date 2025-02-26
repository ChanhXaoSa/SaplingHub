using SH_BusinessObjects.Common.Model.Cart;
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
    public class CartService(ICartRepository CartRepository) : ICartService
    {
        private readonly ICartRepository _CartRepository = CartRepository;

        public async Task<List<Cart>> GetAllAsync()
        {
            return await _CartRepository.GetAllAsync();
        }

        public async Task<Cart?> GetByIdAsync(Guid id)
        {
            return await _CartRepository.GetByIdAsync(id);
        }

        public async Task<Cart> CreateAsync(CartModel cart)
        {
            Cart newCart = new()
            {
                UserId = cart.UserId,
                SaplingId = cart.SaplingId,
                Quantity = cart.Quantity,
            };
            await _CartRepository.AddAsync(newCart);
            return newCart;
        }

        public async Task UpdateAsync(Guid id, CartModel cart)
        {
            var existingCart = await _CartRepository.GetByIdAsync(id);
            if (existingCart == null)
                throw new KeyNotFoundException("Không tìm thấy cây để cập nhật.");

            existingCart.SaplingId = id;
            existingCart.Quantity = cart.Quantity;

            await _CartRepository.UpdateAsync(existingCart);
        }

        public async Task DeleteAsync(Guid id)
        {
            var cart = await _CartRepository.GetByIdAsync(id);
            if (cart == null)
                throw new KeyNotFoundException("Không tìm thấy cây để xóa.");

            await _CartRepository.DeleteAsync(id);
        }
    }
}
