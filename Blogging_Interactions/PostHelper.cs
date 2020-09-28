using Blogging_BackEnd.ServiceUtilities;
using CodeFirst_APP.Blogging;
using CodeFirst_APP.Blogging.ModelExtensions;
using CodeFirst_APP.Blogging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blogging_Interactions
{
    public class PostHelper
    {
        PostService _serv = new PostService(new BloggingContext());

        public BlogExtended DeletePost(BlogExtended currentBlog, Post currentPost)
        {
            _serv.DeletePost(currentPost);
            currentBlog.Posts.Remove(currentPost);
            return currentBlog;
        }

        public List<Post> GetAllPostsForBlogAsList(BlogExtended currentBlog)
        {
            var posts = _serv.GetAllPostsForCurrentBlog(currentBlog).ToList();

            if (posts.Where(x => x.Title == "Add new").Count() == 0)
            {
                posts.Add(new Post() { Title = "Add new" });
            }
            return posts;
        }
    }
}
