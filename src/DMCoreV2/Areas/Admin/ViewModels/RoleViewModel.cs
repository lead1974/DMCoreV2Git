using DMCoreV2.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCoreV2.Areas.Admin.ViewModels
{
    public class RoleViewModel
    {
        public RoleViewModel()
        {

        }

    }

    public class RoleIndex
    {
        public IEnumerable<AuthRole> Roles { get; set; }
    }

    public class RoleNew
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }

    public class RoleEdit
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }

}
