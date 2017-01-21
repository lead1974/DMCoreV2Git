using DMCoreV2.DataAccess.Models.User;
using DMCoreV2.DataAccess.Models.Blog;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCoreV2.DataAccess.Repos
{
    public class BlogRepository : IBlogRepository
    {
        private DMDbContext _context;
        private ILogger<BlogRepository> _logger;
        private readonly UserManager<AuthUser> _userManager;

        public BlogRepository(DMDbContext context, 
            UserManager<AuthUser> userManager,
            ILogger<BlogRepository> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }
        public void AddComment(long postId, string username, BlogComment newComment)
        {
            var thePost = FindPostById(postId);
            newComment.Id = thePost.BlogComments.Max(s => s.Id) + 1;
            thePost.BlogComments.Add(newComment);
            _context.BlogComments.Add(newComment);
        }

        public void AddPost(BlogPost newPost)
        {
            _context.Add(newPost);
        }

        public void AddBlogCategory(BlogCategory newBlogCategory)
        {
            _context.Add(newBlogCategory);
        }

        public IEnumerable<BlogPost> GetAllPosts()
        {
            try
            {
                return _context.BlogPosts.OrderByDescending(t => t.DateToBePosted).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get posts from database", ex);
                return null;
            }
        }

        public IEnumerable<BlogCategory> GetAllBlogCategories()
        {
            try
            {
                return _context.BlogCategories.OrderByDescending(t => t.Name).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get blog categories from database", ex);
                return null;
            }
        }

        public IEnumerable<BlogPost> GetAllPostsWithComments()
        {
            try
            {
                return _context.BlogPosts
                .Include(p => p.BlogComments)
                .OrderByDescending(p => p.DateToBePosted)
                .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get posts with comments from database", ex);
                return null;
            }
        }

        public IEnumerable<BlogPost> GetUserPostsWithComments(string userName)
        {
            try
            {
                return _context.BlogPosts
                .Include(p => p.BlogComments)
                .OrderByDescending(p => p.DateToBePosted)
                .Where(p => p.Author == userName)
                .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get posts with comments from database", ex);
                return null;
            }
        }

        public BlogPost FindPostById(long Id)
        {
            return _context.BlogPosts.Include(p => p.BlogComments)
                           .Where(p => p.Id == Id)
                           .FirstOrDefault();
        }

        public BlogPost FindPostByName(string postName)
        {
            return _context.BlogPosts.Include(p => p.BlogComments)
                           .Where(p => p.Title.Contains(postName))
                           .FirstOrDefault();
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public BlogCategory FindBlogCategoryById(long Id)
        {
            return _context.BlogCategories.Include(p => p.BlogPosts)
                           .Where(p => p.Id == Id)
                           .FirstOrDefault();
        }

        public BlogCategory FindBlogCategoryByName(string blogCategoryName)
        {
            return _context.BlogCategories.Include(p => p.BlogPosts)
                           .Where(p => p.Name.Contains(blogCategoryName))
                           .FirstOrDefault();
        }
    }
}
