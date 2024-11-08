using FluentAssertions;
using SQLite.Database;

namespace SQLiteLab.Tests
{
    public class SqlLiteDbContextTests
    {
        [Fact]
        public void SqlLiteDbContext_ShouldBeAbleToConnect()
        {
            // Arrange, Act
            var sqLiteDbContext = new SqlLiteDbContext();
            sqLiteDbContext.Database.EnsureDeleted();
            sqLiteDbContext.Database.EnsureCreated();


            // Assert
            sqLiteDbContext.Database.CanConnect().Should().BeTrue();
        }

        [Fact]
        public void SqlLiteDbContext_CanInsertAndGetEntity()
        {
            // Arrange
            var sqLiteDbContext = new SqlLiteDbContext();
            sqLiteDbContext.Database.EnsureDeleted();
            sqLiteDbContext.Database.EnsureCreated();

            // Act
            var blog = new Blog
            {
                BlogId = 1,
                Url = "https://www.google.com",
                Title = "Google",
                Content = "Search Engine",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsDeleted = false,
                DeletedAt = DateTime.Now,
                DeletedBy = "Admin",
                CreatedBy = "Admin",
                UpdatedBy = "Admin"
            };
            sqLiteDbContext.Blogs.Add(blog);
            sqLiteDbContext.SaveChanges();

            // Assert
            sqLiteDbContext.Blogs.Count().Should().Be(1);
            sqLiteDbContext.Blogs.Find(blog.BlogId).Should().NotBeNull();
            sqLiteDbContext.Blogs.Find(blog.BlogId).Title.Should().Be(blog.Title);
        }
    }
}