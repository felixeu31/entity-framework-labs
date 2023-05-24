using Microsoft.EntityFrameworkCore;

namespace QueryableBatch.Tests.Infra
{
    public class InMemoryIntegrationTestFixture : IDisposable
    {
        public readonly DbContextOptions<MyDbContext> DbContextOptions;

        public InMemoryIntegrationTestFixture()
        {
            // Set up the DbContextOptions for the in-memory database
            DbContextOptions = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase("QueryableBatchTestDatabase")
                .Options;
        }

        public void Dispose()
        {
        }
    }
}
