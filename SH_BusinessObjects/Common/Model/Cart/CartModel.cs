using SH_BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_BusinessObjects.Common.Model.Cart
{
    public class CartModel
    {
        [Required(ErrorMessage = "UserId required")]
        public required string UserId { get; set; }

        [Required(ErrorMessage = "SaplingId required")]
        public Guid SaplingId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }

        public void Validate()
        {
            if (SaplingId == Guid.Empty)
                throw new ArgumentException("SaplingId cannot be empty.");
        }
    }
}
