using System.Data.SqlClient;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace QueryableBatch.Tests.Infra
{
    public class LocalDbIntegrationTestFixture : IDisposable
    {
        public readonly DbContextOptions<MyDbContext> DbContextOptions;
        private const string TestDatabaseName = "QueryableBatchTestDatabase";

        public LocalDbIntegrationTestFixture()
        {
            // Set up the DbContextOptions for your test database
            DbContextOptions = new DbContextOptionsBuilder<MyDbContext>()
                .UseSqlServer($"Server=.;Database={TestDatabaseName};Trusted_Connection=True;TrustServerCertificate=true;")
                .Options;

            // Create the test database
            CreateTestDatabase();
        }

        public void Dispose()
        {
            // Drop the test database
            DropTestDatabase();
        }

        private void CreateTestDatabase()
        {
            // Create the test database using EF migrations or other suitable methods
            using (var context = new MyDbContext(DbContextOptions))
            {
                context.Database.EnsureCreated();
            }
        }

        private void DropTestDatabase()
        {
            // Drop the test database using EF migrations or other suitable methods
            using (var context = new MyDbContext(DbContextOptions))
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}
