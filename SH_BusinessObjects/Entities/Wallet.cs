using SH_BusinessObjects.Common;
using SH_BusinessObjects.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_BusinessObjects.Entities
{
    public class Wallet : BaseAuditableEntity
    {
        [ForeignKey("ApplicationUser")]
        public required string UserId { get; set; }
        public string? WalletName { get; set; } = "Default Wallet";
        public double Balance { get; set; } = 0.0;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastUpdatedDate { get; set; } = DateTime.Now;

        public virtual ApplicationUser? ApplicationUser { get; set; }
    }
}
