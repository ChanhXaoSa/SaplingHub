namespace UI.Models
{
    public class PlaceBidRequest
    {
        public string? UserId { get; set; }
        public decimal BidAmount { get; set; }
        public Guid AuctionPlantId { get; set; }
    }
}
