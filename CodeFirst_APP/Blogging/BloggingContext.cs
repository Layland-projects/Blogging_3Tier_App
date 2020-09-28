using CodeFirst_APP.Blogging.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CodeFirst_APP.Blogging
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => 
            options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Blogging;Integrated Security=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .HasMany(x => x.Posts)
                .WithOne(x => x.Blog)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Post>()
                .HasMany(x => x.Comments)
                .WithOne(x => x.Post)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public override int SaveChanges()
        {
            try
            {
                var blogs = ChangeTracker.Entries()
                    .Where(x => x.Entity is Blog && 
                    (x.State == EntityState.Modified || x.State == EntityState.Added));

                var posts = ChangeTracker.Entries()
                    .Where(x => x.Entity is Post &&
                    (x.State == EntityState.Modified || x.State == EntityState.Added));

                foreach (var blog in blogs)
                {
                    var curBlog = ((Blog)blog.Entity);
                    if (curBlog.Name == "Add new")
                    {
                        blog.State = EntityState.Detached;
                    }
                }

                foreach (var post in posts)
                {
                    if (((Post)post.Entity).Title == "Add new")
                    {
                        post.State = EntityState.Detached;
                    }
                }
                return base.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                //as this is a 1 user app this can just happen because i call save changes when there is potentially nothing valid to save
                return 0;
            }
        }
    }
}
