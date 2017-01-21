using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCoreV2.DataAccess.Models.Blog
{
    public class BlogPostTag
    {
        public long PostId { get; set; }
        public BlogPost Post { get; set; }

        public long TagId { get; set; }
        public BlogTag Tag { get; set; }
    }
}
