using CodeFirst_APP.Blogging.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeFirst_APP.Blogging.ModelExtensions
{
    public class PostExtended : Post
    {
        public bool HasUnsavedChanges { get; set; }
    }
}
