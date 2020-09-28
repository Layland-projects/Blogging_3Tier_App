using CodeFirst_APP.Blogging;
using CodeFirst_APP.Blogging.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace CodeFirst_APP
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using (var db = new BloggingContext())
            {
                var blogs = db.Blogs.Include(x => x.Posts).ThenInclude(x => x.Comments).ToList().First();

                foreach(var post in blogs.Posts)
                {
                    post.Comments.Add(new Comment() { Title = "I was here!", Body = "First!" });
                }

                db.SaveChanges();

                var checker = db.Blogs.Include(x => x.Posts).ThenInclude(x => x.Comments).ToList();

                foreach (var blog in checker)
                {
                    var output = "";
                    foreach (var prop in blog.GetType().GetProperties())
                    {
                        if (prop.PropertyType.UnderlyingSystemType == typeof(Post) || prop.PropertyType.UnderlyingSystemType == typeof(Comment))
                        {
                            output += $"{prop.Name} ";
                            foreach(var p in prop.GetType().GetProperties())
                            {
                                output += $"{p.Name}: {p.GetValue(prop)}";
                            }
                        }
                        output += $"{prop.Name}: {prop.GetValue(blog)} ";
                    }
                    output += "\n";
                    Console.WriteLine(output);
                }
            }
        }
    }
}
