using System;
using System.Collections.Generic;
using System.Text;

namespace CodeFirst_APP.Blogging.Models
{
    public partial class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual Blog Blog { get; set; }
    }
}
