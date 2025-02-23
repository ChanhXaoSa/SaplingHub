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
    public class Payment : BaseAuditableEntity
    {
        [ForeignKey("Order")]
        public Guid OrderId { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public double Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public virtual Order? Order { get; set; }
    }
}
