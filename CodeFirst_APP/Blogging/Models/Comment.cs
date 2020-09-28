using System;
using System.Collections.Generic;
using System.Text;

namespace CodeFirst_APP.Blogging.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public virtual Post Post { get; set; }
    }
}
