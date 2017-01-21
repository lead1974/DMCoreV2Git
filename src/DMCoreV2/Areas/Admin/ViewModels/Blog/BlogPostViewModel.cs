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
    public class BlogPostViewModel
    {

        public class BlogPostIndex
        {
            public PageData<BlogPost> BlogPosts { get; set; }
        }

        public class BlogPostForm
        {
            public long Id { get; set; }
            public bool IsNew { get; set; }
            //private string _key;
            //public string Key
            //{
            //    get
            //    {
            //        if (_key == null)
            //        {
            //            _key = Regex.Replace(Title.ToLower(), "[^a-z0-9]", "-");
            //        }
            //        return _key;
            //    }
            //    set { _key = value; }
            //}

            [Display(Name = "Post Title")]
            [Required]
            [DataType(DataType.Text)]
            [StringLength(100, MinimumLength = 5,
                ErrorMessage = "Title must be between 5 and 100 characters long")]
            public string Title { get; set; }

            [Display(Name = "Post Slug")]
            [Required]
            [DataType(DataType.Text)]
            [StringLength(100, MinimumLength = 5,  ErrorMessage = "Slug must be between 5 and 100 characters long")]
            public string Slug { get; set; }

            public string Author { get; set; }

            [Required]
            [MinLength(100, ErrorMessage = "Blog posts must be at least 100 characters long")]
            [DataType(DataType.MultilineText)]
            public string Body { get; set; }

            public DateTime DateCreated { get; set; }
            public DateTime DateUpdated { get; set; }
            public DateTime DateToBePosted { get; set; }
            public bool Approved { get; set; }
            public long UpVote { get; set; }
            public long DownVote { get; set; }
            public long BlogCategoryId { get; set; }
            public string Tags { get; set; }
        }

    }
}
