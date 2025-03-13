using SH_BusinessObjects.Entities;
using SH_BusinessObjects.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_BusinessObjects.Common.Model.Payment
{
    public class PaymentModel
    {
        [Required(ErrorMessage = "OrderId required")]
        public Guid OrderId { get; set; }

        [Required(ErrorMessage = "PaymentMethod required")]
        public PaymentMethod PaymentMethod { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "PaymentDate required")]
        public DateTime PaymentDate { get; set; }

        [Required(ErrorMessage = "PaymentStatus required")]
        public PaymentStatus PaymentStatus { get; set; }

        public void Validate()
        {
            if (OrderId == Guid.Empty)
                throw new ArgumentException("OrderId cannot be empty.");
        }
    }
}
