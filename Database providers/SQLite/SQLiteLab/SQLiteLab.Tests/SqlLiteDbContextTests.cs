using FluentAssertions;
using SQLite.Database;

namespace SQLiteLab.Tests;

public class SqlLiteDbContextTests : IDisposable
{
    private readonly SqlLiteDbContext _dbContext;
    private readonly ISqlLiteDbContextFixture _sqlLiteInDiskDbContextFixture;

    public SqlLiteDbContextTests()
    {
        _sqlLiteInDiskDbContextFixture = new SqlLiteInMemoryDbContextFixture();
        _dbContext = _sqlLiteInDiskDbContextFixture.DbContext;
    }

    [Fact]
    public void SqlLiteDbContext_ShouldBeAbleToConnect()
    {
        // Assert the connection is successful
        _dbContext.Database.CanConnect().Should().BeTrue();
    }

    [Fact]
    public void SqlLiteDbContext_CanInsertAndGetEntity()
    {
        // Arrange
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

        // Act
        _dbContext.Blogs.Add(blog);
        _dbContext.SaveChanges();

        // Assert
        var retrievedBlog = _dbContext.Blogs.Find(blog.BlogId);
        retrievedBlog.Should().NotBeNull();
        retrievedBlog!.Title.Should().Be(blog.Title);
    }

    public void Dispose()
    {
        _sqlLiteInDiskDbContextFixture.Dispose();
    }
}