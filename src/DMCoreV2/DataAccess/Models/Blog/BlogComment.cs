using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DMCoreV2.DataAccess.Models.Blog
{
    public class BlogComment
    {
        public long Id { get; set; }
        [Required]
        public string Body { get; set; }
        public bool Approved { get; set; }
        public long UpVote { get; set; }
        public long DownVote { get; set; }
        public string NestedCommentId { get; set; }
        public long PostId { get; set; }
        public string Author { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
