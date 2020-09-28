using CodeFirst_APP.Blogging.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeFirst_APP.Blogging.ModelExtensions
{ 
    public class BlogExtended : Blog
    {
        public bool IsInEditMode { get; set; }
    }
}
