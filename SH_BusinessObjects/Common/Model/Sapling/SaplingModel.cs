using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_BusinessObjects.Common.Model.Sapling
{
    public class SaplingModel
    {
        [Required(ErrorMessage = "Name required")]
        [Length(0, 100, ErrorMessage = "Name must be less than 100 characters")]
        public required string Name { get; set; }

        [Length(0, 100, ErrorMessage = "Description must be less than 500 characters")]
        public string? Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public double Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be greater than 0")]
        public int StockQuantity { get; set; }

        public Guid CategoryId { get; set; }

        public string? ImageUrl { get; set; }
    }
}
