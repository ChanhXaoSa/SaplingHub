using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_BusinessObjects.Common.Model.Sapling
{
    public class SaplingModel
    {
        public required string Name { get; set; }

        public string? Description { get; set; }

        public double Price { get; set; }

        public int StockQuantity { get; set; }

        public Guid CategoryId { get; set; }

        public string? ImageUrl { get; set; }
    }
}
