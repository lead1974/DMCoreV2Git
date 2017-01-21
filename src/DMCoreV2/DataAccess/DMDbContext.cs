using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DMCoreV2.ViewModels;
using DMCoreV2.DataAccess.Models.User;
using DMCoreV2.DataAccess.Models.Blog;

namespace DMCoreV2.DataAccess
{
    public class DMDbContext : IdentityDbContext<AuthUser,AuthRole,string>
    {
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<BlogPostTag> BlogPostTags { get; set; }

        public DMDbContext(DbContextOptions<DMDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //To navigate, use a Select:
            // person.Photos
            //var tags = post.PostTags.Select(c => c.Tag);
            builder.Entity<BlogPostTag>().HasKey(x => new { x.PostId, x.TagId });
            base.OnModelCreating(builder);
        }
    }
}
