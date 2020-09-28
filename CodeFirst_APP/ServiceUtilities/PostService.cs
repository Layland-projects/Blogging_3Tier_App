using CodeFirst_APP.Blogging;
using CodeFirst_APP.Blogging.ModelExtensions;
using CodeFirst_APP.Blogging.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blogging_BackEnd.ServiceUtilities
{
    public class PostService
    {
        BloggingContext _db;
        public PostService(BloggingContext db)
        {
            _db = db;
        }

        public void DeletePost(Post currentPost)
        {
            var existingPost = _db.Posts.Where(x => x.PostId == currentPost.PostId).FirstOrDefault();
            if (existingPost != null)
            {
                //foreach (var comment in existingPost.Comments)
                //{
                //    _db.Comments.Remove(comment);
                //}
                _db.Posts.Remove(existingPost);
                _db.SaveChanges();
            }
        }

        public IEnumerable<Post> GetAllPostsForCurrentBlog(BlogExtended currentBlog)
        {
            return _db.Posts.Include(x => x.Blog).Where(x => x.Blog == currentBlog);
        }
    }
}
