using System;
using System.Data.Common;
using System.IO;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using SQLite.Database;

public class SqlLiteInMemoryDbContextFixture : ISqlLiteDbContextFixture
{
    private DbConnection _connection;
    public SqlLiteDbContext DbContext { get; private set; }
    private string _databaseFilePath;

    public SqlLiteInMemoryDbContextFixture()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
            .UseSqlite(_connection).Options;

        DbContext = new SqlLiteDbContext(options);

        DbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        _connection.Dispose();
        _connection = null;
    }
}