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
}
