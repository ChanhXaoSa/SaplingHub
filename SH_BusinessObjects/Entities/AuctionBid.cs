﻿using SH_BusinessObjects.Common;
using SH_BusinessObjects.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_BusinessObjects.Entities
{
    public class AuctionBid : BaseAuditableEntity
    {
        public string? BidderName { get; set; }

        [ForeignKey("ApplicationUser")]
        public required string UserId { get; set; }
        public required decimal BidAmount { get; set; }
        public DateTime BidTime { get; set; } = DateTime.UtcNow;
        public bool IsWinningBid { get; set; } = false;

        [ForeignKey("AuctionPlant")]
        public Guid AuctionPlantId { get; set; }

        public virtual ApplicationUser? ApplicationUser { get; set; }
        public virtual AuctionPlant? AuctionPlant { get; set; }
    }
}
