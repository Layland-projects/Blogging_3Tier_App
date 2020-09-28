using CodeFirst_APP.Blogging.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogging_BackEnd.Blogging.Structs
{
    public struct BlogRollback
    {
        public int BlogId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
