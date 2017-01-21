using DMCoreV2.DataAccess.Models.Blog;
using DMCoreV2.Services;
using DMCoreV2.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCoreV2.ViewModels.BlogViewModels
{
    public class PostIndex
    {
        public IEnumerable<BlogPost> BlogPosts { get; set; }
    }
    public class BlogPostViewModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Body { get; set; }
        public DateTime Posted { get; set; }
    }
}
