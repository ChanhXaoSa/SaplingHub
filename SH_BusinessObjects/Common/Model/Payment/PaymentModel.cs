using SH_BusinessObjects.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_BusinessObjects.Common.Model.Payment
{
    public class PaymentModel
    {
        public Guid OrderId { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public double Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public PaymentStatus PaymentStatus { get; set; }
    }
}
