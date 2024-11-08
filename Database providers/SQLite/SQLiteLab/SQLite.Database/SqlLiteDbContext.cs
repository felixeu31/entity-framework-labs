using Microsoft.EntityFrameworkCore;

namespace SQLite.Database;

public class SqlLiteDbContext : DbContext
{
    public SqlLiteDbContext(DbContextOptions<SqlLiteDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>().ToTable("Blogs");

    }

    public DbSet<Blog> Blogs { get; set; }
}

public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime DeletedAt { get; set; }
    public string DeletedBy { get; set; }
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }

}