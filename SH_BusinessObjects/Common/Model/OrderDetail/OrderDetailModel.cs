using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_BusinessObjects.Common.Model.OrderDetail
{
    public class OrderDetailModel
    {
        [Required(ErrorMessage = "OrderId required")]
        public Guid OrderId { get; set; }

        [Required(ErrorMessage = "SaplingId required")]
        public Guid SaplingId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "UnitPrice must be greater than 0")]
        public double UnitPrice { get; set; }

        public void Validate()
        {
            if (OrderId == Guid.Empty)
                throw new ArgumentException("OrderId cannot be empty.");
            if (SaplingId == Guid.Empty)
                throw new ArgumentException("SaplingId cannot be empty.");
        }
    }
}
