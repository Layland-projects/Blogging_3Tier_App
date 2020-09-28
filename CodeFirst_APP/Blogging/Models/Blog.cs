using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CodeFirst_APP.Blogging.Models
{
    public class Blog
    {
        public int BlogId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Url { get; set; }
        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
