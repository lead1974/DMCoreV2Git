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

namespace DMCoreV2.Areas.Admin.Controllers.Blog
{
    [Area("Admin")]
    [Route("admin/[controller]")]
    [Authorize(Roles = RoleName.CanManageBlog)]
    public class BlogCategoryController : Controller
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

        public BlogCategoryController(
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
            _logger = loggerFactory.CreateLogger <BlogCategoryController>();
        }

        [HttpGet, Route("blogCategoryIndex")]
        public IActionResult BlogCategoryIndex(int page = 1)
        {
            var totalBlogCategoryCount = _blogRepo.GetAllBlogCategories().ToList().Count();
            var currentBlogCategoryPage = _blogRepo.GetAllBlogCategories()
                .OrderByDescending(c => c.Name)
                .Skip((page - 1) * PostsPerPage)
                .Take(PostsPerPage)
                .ToList();

            return View(new BlogCategoryIndex
            {
                BlogCategories = new PageData<BlogCategory>(currentBlogCategoryPage, totalBlogCategoryCount, page, PostsPerPage)
            });
        }

        [HttpGet, Route("blogCategoryForm")]
        public IActionResult BlogCategoryForm(string returnUrl, string action)
        {
            var category = new BlogCategoryForm
            {
                IsNew = true
            };
            return View("Form", category);
        }


        [HttpPost, Route("blogCategoryForm"), ValidateAntiForgeryToken]
        public IActionResult CategoryForm(BlogCategoryForm form, string returnUrl, string action)
        {
            if (action == "CreateBlogCategory" || action == "UpdateBlogCategory")
            {
                form.IsNew = _globalService.IsNullOrDefault(form.Id);
                if (!ModelState.IsValid) return View(form);

                BlogCategory blogCategory;
                if (form.IsNew)
                {
                    blogCategory = new BlogCategory
                    {
                        DateCreated = DateTime.UtcNow,
                        Author = User.Identity.Name,
                        CreatedBy = User.Identity.Name,
                    };
                }
                else
                {
                    blogCategory = _blogRepo.FindBlogCategoryById(form.Id);
                    if (blogCategory == null)
                    {
                        ModelState.AddModelError("", "Blog Category Update failed: Blog Category not found!");
                    }

                    blogCategory.DateUpdated = DateTime.UtcNow;
                    blogCategory.UpdatedBy = User.Identity.Name;

                }

                blogCategory.Name = form.Name;

                _blogRepo.AddBlogCategory(blogCategory);
                _blogRepo.SaveAll();
            }
            else if (action == "Cancel")
                return RedirectToAction("blogCategoryIndex", "blogcategory");

            return RedirectToAction("blogCategoryIndex", "blogcategory");
        }
    }
}
