using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DMCoreV2.DataAccess.Models.Blog;
using DMCoreV2.ViewModels.BlogViewModels;
using DMCoreV2.DataAccess.Repos;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DMCoreV2.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        private BlogRepository _blogRepository;

        public BlogController(BlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var posts = new[]
{
                new BlogPostViewModel
                {
                    Title = "My blog post",
                    Posted = DateTime.Now,
                    Author = "Jess Chadwick",
                    Body = "This is a great blog post, don't you think?"
                },
                new BlogPostViewModel
                {
                    Title = "My second blog post",
                    Posted = DateTime.Now,
                    Author = "Jess Chadwick",
                    Body = "This is ANOTHER great blog post, don't you think?"
                },
            };

            return View(posts);
        }

        [Route("{year:min(2016)}/{month:range(1,12)}/{key}")]
        public IActionResult Post(int year, int month, string key)
        {
            var post = new BlogPostViewModel
            {
                Title = "My blog post",
                Posted = DateTime.Now,
                Author = "Jess Chadwick",
                Body = "This is a great blog post, don't you think?"
            };

            return View(post);
        }
    }
}
