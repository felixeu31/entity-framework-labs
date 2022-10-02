using SqlServerEntityFramework.Configurations;
using SqlServerEntityFramework.Model;
using System.Data.Entity;
using System.Reflection;

namespace SqlServerEntityFramework.Context
{
    internal class BloggingContext : DbContext
    {
        public BloggingContext() : base("BlogContext")
        {
            
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected sealed override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BlogEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new PostEntityTypeConfiguration());
            
            //modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(BloggingContext)));
        }
    }
}
