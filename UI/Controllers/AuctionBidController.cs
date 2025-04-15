using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SH_Services.Services.Interfaces;
using UI.Models;

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionBidController(IAuctionBidService auctionBidService) : ControllerBase
    {
        private readonly IAuctionBidService _auctionBidService = auctionBidService;

        [HttpPost]
        public async Task<IActionResult> PlaceBid([FromBody] PlaceBidRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.UserId) || request.BidAmount <= 0)
            {
                return BadRequest("Invalid bid request.");
            }

            var (success, message) = await _auctionBidService.PlaceBidAsync(request.AuctionPlantId, request.UserId, request.BidAmount);

            if (success)
            {
                return Ok(message);
            }
            else
            {
                return BadRequest(message);
            }
        }
    }
}
