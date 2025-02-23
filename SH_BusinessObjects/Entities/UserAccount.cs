using SH_BusinessObjects.Common;
using SH_BusinessObjects.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_BusinessObjects.Entities
{
    public class UserAccount : BaseAuditableEntity
    {
        [ForeignKey("ApplicationUser")]
        public required string UserAccountId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
    }
}
