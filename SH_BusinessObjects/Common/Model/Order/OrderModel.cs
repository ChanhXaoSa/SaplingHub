using SH_BusinessObjects.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_BusinessObjects.Common.Model.Order
{
    public class OrderModel
    {
        [Required(ErrorMessage = "UserId required")]
        public required string UserId { get; set; }

        [Required(ErrorMessage = "OrderDate required")]
        public DateTime OrderDate { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "TotalAmount must be greater than 0")]
        public double TotalAmount { get; set; }

        [Required(ErrorMessage = "Status required")]
        public OrderStatus Status { get; set; }

        [Required(ErrorMessage = "ShippingAddress required")]
        public required string ShippingAddress { get; set; }
    }
}
