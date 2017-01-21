using DMCoreV2.DataAccess.Models.User;
using DMCoreV2.DataAccess.Models.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DMCoreV2.DataAccess.Repos
{
    public interface IEntityRepository<T>
    {
        IQueryable<T> All { get; }
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProeprties);
        T Find(int id);
        void insertOrUpdate(T entity);
        void Delete(int id);
        void Save();
    }

    public interface IUserRepository<T> : IEntityRepository<AuthUser>
    {
    }
    public interface IBlogRepository
    {
        IEnumerable<BlogPost> GetAllPosts();
        IEnumerable<BlogCategory> GetAllBlogCategories();
        IEnumerable<BlogPost> GetAllPostsWithComments();
        IEnumerable<BlogPost> GetUserPostsWithComments(string userName);
        void AddPost(BlogPost newPost);
        void AddBlogCategory(BlogCategory newBlogCategory);
        bool SaveAll();
        BlogPost FindPostById(long Id);
        BlogCategory FindBlogCategoryById(long Id);
        BlogPost FindPostByName(string postName);
        BlogCategory FindBlogCategoryByName(string blogCategoryName);
        void AddComment(long postId, string username, BlogComment newComment);        
    }
}
