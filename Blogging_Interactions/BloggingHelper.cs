using Blogging_BackEnd.Blogging.Structs;
using Blogging_BackEnd.ServiceUtilities;
using CodeFirst_APP.Blogging;
using CodeFirst_APP.Blogging.ModelExtensions;
using CodeFirst_APP.Blogging.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;

namespace Blogging_Interactions
{
    public class BloggingHelper
    {
        BloggingService _serv = new BloggingService(new BloggingContext());

        public List<Blog> GetAllBlogsAsList()
        {
            return _serv.GetAllBlogs().ToList();
        }

        public List<BlogExtended> GetAllExtendedBlogsAsList()
        {
            var blogs = new List<BlogExtended>();
            foreach (var blog in _serv.GetAllBlogs())
            {
                blogs.Add(new BlogExtended()
                {
                    Name = blog.Name,
                    BlogId = blog.BlogId,
                    Posts = blog.Posts,
                    IsInEditMode = false,
                    Url = blog.Url
                });
            }
            blogs.Add(new BlogExtended() { Name = "Add new" });
            foreach (var blog in blogs.Where(x => x.Posts.Where(x => x.Title == "Add new").Count() == 0))
            {
                var posts = blog.Posts;
                posts.Add(new Post() { Title = "Add new" });
            }
            return blogs;
        }

        public void SaveBlog(Blog blog)
        {
            if (blog.Name != "Add new")
            {
                _serv.SaveBlog(blog);
            }
        }
        public void RevertBlog(BlogExtended currentBlog)
        {
            _serv.RevertBlogs();
        }

        public void DeleteBlog(BlogExtended currentBlog)
        {
            _serv.DeleteBlog(currentBlog);
        }
    }
}
