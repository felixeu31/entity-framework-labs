using System;
using System.IO;

using Microsoft.EntityFrameworkCore;

using SQLite.Database;

public class SqlLiteInDiskDbContextFixture : ISqlLiteDbContextFixture
{
    public SqlLiteDbContext DbContext { get; private set; }
    private string _databaseFilePath;

    public SqlLiteInDiskDbContextFixture()
    {
        // Create a unique filename for each test database
        _databaseFilePath = Path.Combine(Path.GetTempPath(), $"TestDb_{Guid.NewGuid()}.sqlite");

        // Configure DbContext options to use the SQLite database file
        var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
            .UseSqlite($"Data Source={_databaseFilePath}")
            .Options;

        // Initialize the DbContext with these options
        DbContext = new SqlLiteDbContext(options);

        // Ensure the database schema is created
        DbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        DbContext.Database.CloseConnection();
        DbContext.Database.EnsureDeleted();
    }
}