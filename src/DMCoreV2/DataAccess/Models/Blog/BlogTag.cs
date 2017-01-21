using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCoreV2.DataAccess.Models.Blog
{
    public class BlogTag
    {
        public long Id { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public ICollection<BlogPostTag> BlogPostTags { get; set; }
    }
}
