using DMCoreV2.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCoreV2.Areas.Admin.ViewModels
{
    public class UserViewModel
    {
    }

    public class UserIndex
    {
        public IEnumerable<AuthUser> Users { get; set; }
    }

    public class UserNew
    {
        [Required, MaxLength(128), DataType(DataType.Text)]
        [StringLength(128, MinimumLength = 4, ErrorMessage = "User Name must be between 4 and 128 characters long")]
        [Display(Name = "User Name")]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        [StringLength(256, MinimumLength = 5, ErrorMessage = "Email Address must be between 4 and 256 characters long")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Email Confirmed")]
        public bool EmailConfirmed { get; set; }
    }

    public class UserEdit
    {
        public string Id { get; set; }
        [Required, MaxLength(128), DataType(DataType.Text)]
        [StringLength(128, MinimumLength = 4, ErrorMessage = "User Name must be between 4 and 128 characters long")]
        [Display(Name = "User Name")]
        public string Username { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Email Confirmed")]
        public bool EmailConfirmed { get; set; }
    }

    public class UserResetPassword
    {
        [Display(Name = "User Name")]
        public string Username { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
