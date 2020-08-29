using FooBlog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;

namespace FooBlog.Data
{
    public class FooBlogContext: DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }


        public FooBlogContext() { }

        public FooBlogContext (DbContextOptions<FooBlogContext> options) :base(options)
        { }
    
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=localhost;Database=FooBlog;Trusted_Connection=True;MultipleActiveResultSets=true;");
            options.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<BlogCategory>()
            //        .HasKey(bc => new { bc.BlogID, bc.CategoryID });
            //modelBuilder.Entity<BlogCategory>()
            //    .HasOne(bc => bc.Blog)
            //    .WithMany(b => b.Categories)
            //    .HasForeignKey(bc => bc.BlogID);
            //modelBuilder.Entity<BlogCategory>()
            //    .HasOne(bc => bc.Category)
            //    .WithMany(c => c.BlogCategories)
            //    .HasForeignKey(bc => bc.CategoryID);
            //modelBuilder.Entity<Comment>()
            //            .HasOne(c => c.Blog)
            //            .WithMany(b => b.Comments)
            //            .HasForeignKey(c => c.BlogID);
            base.OnModelCreating(modelBuilder);
        }
    }
}
