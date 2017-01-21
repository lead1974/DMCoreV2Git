using DMCoreV2.DataAccess.Models.Blog;
using DMCoreV2.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DMCoreV2.Areas.Admin.ViewModels.Blog
{
    public class BlogCategoryViewModel
    {

        public class BlogCategoryIndex
        {
            public PageData<BlogCategory> BlogCategories { get; set; }
        }

        public class BlogCategoryForm
        {
            public long Id { get; set; }
            public bool IsNew { get; set; }

            [Display(Name = "Blog Category Name")]
            [Required]
            [DataType(DataType.Text)]
            public string Name { get; set; }

            public string Author { get; set; }
            public DateTime DateCreated { get; set; }
            public DateTime DateUpdated { get; set; }
            public string CreatedBy { get; set; }
            public string UpdatedBy { get; set; }
        }

    }
}
