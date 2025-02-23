using SH_BusinessObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_BusinessObjects.Entities
{
    public class Category : BaseAuditableEntity
    {
        public required string Name { get; set; }

        public string? Description { get; set; }

        public virtual IList<Sapling>? Saplings { get; set; }
    }
}
