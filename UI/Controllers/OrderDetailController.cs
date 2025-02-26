using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SH_BusinessObjects.Common.Model.OrderDetail;
using SH_Services.Services.Interfaces;

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orderDetails = await _orderDetailService.GetAllAsync();
            return Ok(orderDetails);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var orderDetail = await _orderDetailService.GetByIdAsync(id);
            if (orderDetail == null)
                return NotFound();
            return Ok(orderDetail);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderDetailModel orderDetail)
        {
            try
            {
                var createdOrderDetail = await _orderDetailService.CreateAsync(orderDetail);
                return CreatedAtAction(nameof(GetById), new { id = createdOrderDetail.Id }, createdOrderDetail);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] OrderDetailModel orderDetail)
        {
            try
            {
                await _orderDetailService.UpdateAsync(id, orderDetail);
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
                await _orderDetailService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
