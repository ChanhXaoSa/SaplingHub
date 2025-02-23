using Microsoft.AspNetCore.Identity;
using SH_BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_BusinessObjects.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }

        public string? Address { get; set; }

        public string? ImageUrl { get; set; }

        public virtual IList<AdminAccount>? AdminAccounts { get; set; }

        public virtual IList<UserAccount>? UserAccounts { get; set; }
    }
}
