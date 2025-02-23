using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SH_BusinessObjects.Common;
using SH_BusinessObjects.Enum;
using SH_BusinessObjects.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_BusinessObjects.Entities
{
    public class Order : BaseAuditableEntity
    {
        [ForeignKey("ApplicationUser")]
        public required string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public required string ShippingAddress { get; set; }

        public virtual ApplicationUser? ApplicationUser { get; set; }

        public virtual IList<OrderDetail>? OrderDetails { get; set; }
    }
}
