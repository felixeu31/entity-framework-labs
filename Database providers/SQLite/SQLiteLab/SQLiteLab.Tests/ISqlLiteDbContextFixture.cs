using SQLite.Database;

public interface ISqlLiteDbContextFixture : IDisposable
{
    SqlLiteDbContext DbContext { get; }
    void Dispose();
}