using DMCoreV2.DataAccess.Models.Blog;
using DMCoreV2.DataAccess.Models.User;
using DMCoreV2.DataAccess.Repos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCoreV2.api.Blog
{
    [Route("api/blog/[controller]")]
    public class BlogCategoryController : Controller
    {
        private readonly UserManager<AuthUser> _userManager;
        private readonly SignInManager<AuthUser> _signInManager;
        private readonly RoleManager<AuthRole> _roleManager;
        private readonly ILogger _logger;
        private IBlogRepository _blogRepo;

        public BlogCategoryController(
            IBlogRepository blogRepo,
            UserManager<AuthUser> userManager,
            SignInManager<AuthUser> signInManager,
            RoleManager<AuthRole> roleManager,
            ILoggerFactory loggerFactory)
        {
            _blogRepo = blogRepo;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = loggerFactory.CreateLogger<BlogCategoryController>();
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<BlogCategory> Get()
        {
            return _blogRepo.GetAllBlogCategories();
        }
    }
}
