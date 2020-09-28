using Blogging_BackEnd.Blogging.Structs;
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
    public class BloggingService
    {
        BloggingContext _db;
        public BloggingService(BloggingContext db)
        {
            _db = db;
        }

        public IEnumerable<Blog> GetAllBlogs()
        {
            return _db.Blogs.Include(x => x.Posts).ThenInclude(x => x.Comments);
        }

        public Blog GetBlogFromID(int id)
        {
            return _db.Blogs.Include(x => x.Posts).ThenInclude(x => x.Comments).Where(x => x.BlogId == id).FirstOrDefault();
        }

        public void SaveBlog (Blog blog)
        {
            var record = GetBlogFromID(blog.BlogId);
            if (record != null)
            {
                foreach (var prop in record.GetType().GetProperties())
                {
                    prop.SetValue(record, prop.GetValue(blog));
                }
            }
            else
            {
                _db.Blogs.Add(blog);
            }
            _db.SaveChanges();
        }
        public void RevertBlogs()
        {
            _db.Dispose();
            _db = new BloggingContext();
        }

        public void DeleteBlog(BlogExtended currentBlog)
        {
            var existingBlog = _db.Blogs.Where(x => x.BlogId == currentBlog.BlogId).FirstOrDefault();
            if (existingBlog != null)
            {
                //foreach (var post in existingBlog.Posts)
                //{
                //    foreach (var comment in post.Comments)
                //    {
                //        _db.Comments.Remove(comment);
                //    }
                //    _db.SaveChanges();
                //    _db.Posts.Remove(post);
                //}
                //_db.SaveChanges();
                _db.Blogs.Remove(existingBlog);
                _db.SaveChanges();
            }
        }
    }
}
