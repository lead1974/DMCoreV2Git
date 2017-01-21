using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCoreV2.DataAccess.Models.Blog
{
    public class BlogCategory
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Author { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}
