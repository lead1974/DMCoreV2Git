using DMCoreV2.DataAccess;
using DMCoreV2.DataAccess.Models.User;
using DMCoreV2.DataAccess.Models.Blog;
using DMCoreV2.DataAccess.Repos;
using DMCoreV2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DMCoreV2.Areas.Admin.ViewModels.Blog.BlogCategoryViewModel;
using static DMCoreV2.Areas.Admin.ViewModels.Blog.BlogPostViewModel;

namespace DMCoreV2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]")]
    [Authorize(Roles = RoleName.CanManageBlog)]
    public class BlogPostController : Controller
    {
        private readonly UserManager<AuthUser> _userManager;
        private readonly SignInManager<AuthUser> _signInManager;
        private readonly RoleManager<AuthRole> _roleManager;
        private readonly IMailService _emailSender;
        private readonly ISmsService _smsSender;
        private readonly ILogger _logger;

        private IBlogRepository _blogRepo;

        private const int PostsPerPage = 5;
        private GlobalService _globalService;

        public BlogPostController(
            IBlogRepository blogRepo,
            UserManager<AuthUser> userManager,
            SignInManager<AuthUser> signInManager,
            RoleManager<AuthRole> roleManager,
            IMailService emailSender,
            ISmsService smsSender,
            GlobalService globalService,
            ILoggerFactory loggerFactory)
        {
            _blogRepo = blogRepo;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _globalService = globalService;
            _logger = loggerFactory.CreateLogger<BlogPostController>();
        }

        [HttpGet, Route("")]
        public IActionResult Index(int page =1)
        {
            var totalPostCount = _blogRepo.GetAllPosts().ToList().Count();
            var currentPostPage = _blogRepo.GetAllPosts()
                .OrderByDescending(c => c.DateToBePosted)
                .Skip((page - 1) * PostsPerPage)
                .Take(PostsPerPage)
                .ToList();

            return View(new BlogPostIndex
            {
                BlogPosts = new PageData<BlogPost>(currentPostPage, totalPostCount,page,PostsPerPage)
            });
        }

        [HttpGet, Route("blogpostform")]
        public IActionResult EditPost(string returnUrl, string action)
        {          
            var post = new BlogPostForm
            {
                IsNew = true
            };
            return View("BlogPostForm",post);
        }

        [HttpPost, Route("blogpostform"),ValidateAntiForgeryToken]
        public IActionResult Form(BlogPostForm form, string returnUrl, string action)
        {
            if (action == "CreatePost" || action == "UpdatePost")
            {
                form.IsNew = _globalService.IsNullOrDefault(form.Id);
                if (!ModelState.IsValid) return View(form);

                BlogPost post;
                if (form.IsNew)
                {
                    post = new BlogPost
                    {
                        DateToBePosted = DateTime.UtcNow,
                        DateCreated = DateTime.UtcNow,
                        Author = User.Identity.Name,
                        CreatedBy = User.Identity.Name,
                    };
                }
                else
                {
                    post = _blogRepo.FindPostById(form.Id);
                    if (post == null)
                    {
                        ModelState.AddModelError("", "Post update failed: Post not found");
                    }

                    post.DateUpdated = DateTime.UtcNow;
                    post.UpdatedBy = User.Identity.Name;

                }

                post.Title = form.Title;
                post.Slug = form.Slug;
                post.Body = form.Body;
                post.DateToBePosted = form.DateToBePosted;
                post.Approved = form.Approved;
                post.Author = User.Identity.Name;
                post.DateToBePosted = form.DateToBePosted;
                post.DownVote = form.DownVote;
                post.UpVote = form.UpVote;

                _blogRepo.AddPost(post);
                _blogRepo.SaveAll();
            }
            else if (action == "Cancel")
                return RedirectToAction("index", "blogpost");

            return RedirectToAction("index", "blogpost");
        } 
    }
}
