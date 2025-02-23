using SH_BusinessObjects.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_BusinessObjects.Entities
{
    public class Sapling : BaseAuditableEntity
    {
        public required string Name { get; set; }

        public string? Description { get; set; }

        public double Price { get; set; }

        public int StockQuantity { get; set; }

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }

        public string? ImageUrl { get; set; }

        public virtual Category? Category { get; set; }
    }
}
