using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_BusinessObjects.Common.Model.Category
{
    public class CategoryModel
    {
        [Required(ErrorMessage = "Name required")]
        public required string Name { get; set; }

        [Length(0, 100, ErrorMessage = "Description must be less than 500 characters")]
        public string? Description { get; set; }
    }
}
