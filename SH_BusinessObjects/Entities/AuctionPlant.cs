using SH_BusinessObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_BusinessObjects.Entities
{
    public class AuctionPlant : BaseAuditableEntity
    {
        public required string Name { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; } = false;

        public DateTime AuctionStartDate { get; set; }

        public DateTime AuctionEndDate { get; set; }

        public decimal StartingPrice { get; set; }

        public decimal? CurrentHighestBid { get; set; }

        public virtual ICollection<AuctionBid> AuctionBids { get; set; } = [];
    }
}
