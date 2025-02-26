using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SH_BusinessObjects.Common.Model.Cart;
using SH_Services.Services.Interfaces;

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(ICartService cartService) : ControllerBase
    {
        private readonly ICartService _cartService = cartService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var carts = await _cartService.GetAllAsync();
            return Ok(carts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var cart = await _cartService.GetByIdAsync(id);
            if (cart == null)
                return NotFound();
            return Ok(cart);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CartModel cart)
        {
            try
            {
                var createdCart = await _cartService.CreateAsync(cart);
                return CreatedAtAction(nameof(GetById), new { id = createdCart.Id }, createdCart);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CartModel cart)
        {
            try
            {
                await _cartService.UpdateAsync(id, cart);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _cartService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
