using SH_BusinessObjects.Common;
using SH_BusinessObjects.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_BusinessObjects.Entities
{
    public class WalletTransaction : BaseAuditableEntity
    {
        [ForeignKey("Wallet")]
        public Guid WalletId { get; set; }
        public double Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public string? Description { get; set; }
        [ForeignKey("AuctionPlant")]
        public Guid? AuctionPlantId { get; set; }

        public virtual Wallet? Wallet { get; set; }
        public virtual AuctionPlant? AuctionPlant { get; set; }
    }
}
