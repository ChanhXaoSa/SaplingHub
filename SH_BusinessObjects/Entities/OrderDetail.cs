using SH_BusinessObjects.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_BusinessObjects.Entities
{
    public class OrderDetail : BaseAuditableEntity
    {
        [ForeignKey("Order")]
        public Guid OrderId { get; set; }

        [ForeignKey("Sapling")]
        public Guid SaplingId { get; set; }

        public int Quantity { get; set; }

        public double UnitPrice { get; set; }

        public virtual Order? Order { get; set; }

    }
}
