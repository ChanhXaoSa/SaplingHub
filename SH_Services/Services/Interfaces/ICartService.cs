using SH_BusinessObjects.Common.Model.Cart;
using SH_BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_Services.Services.Interfaces
{
    public interface ICartService
    {
        Task<List<Cart>> GetAllAsync();
        Task<Cart?> GetByIdAsync(Guid id);
        Task<Cart> CreateAsync(CartModel cart);
        Task UpdateAsync(Guid id, CartModel cart);
        Task DeleteAsync(Guid id);
    }
}
