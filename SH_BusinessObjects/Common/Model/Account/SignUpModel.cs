using SH_BusinessObjects.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_BusinessObjects.Common.Model.Account
{
    public class SignUpModel
    {
        [Required(ErrorMessage = "UserName required")]
        [Length(0, 100, ErrorMessage = "UserName must be less than 50 characters")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Email required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password required")]
        [Length(6, 100, ErrorMessage = "Password must be between 6 and 100 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must contain at least 1 uppercase letter, 1 lowercase letter, 1 number and 1 special character")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword required")]
        [Compare("Password", ErrorMessage = "Password and ConfirmPassword do not match")]
        public required string ConfirmPassword { get; set; }

        public SignUpStatus Status { get; set; }
    }
}
