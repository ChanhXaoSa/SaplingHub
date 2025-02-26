using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_BusinessObjects.Common.Model.Cart
{
    public class CartModel
    {
        public required string UserId { get; set; }
        public Guid SaplingId { get; set; }
        public int Quantity { get; set; }
    }
}
