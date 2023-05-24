using Bogus;
using FluentAssertions;
using QueryableBatch.Tests.Unit;
using System.Data.SqlClient;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace QueryableBatch.Tests.Integration
{
    public class QueryableBatchGeneratorIntegrationTest : IClassFixture<IntegrationTestFixture>
    {
        private readonly IntegrationTestFixture _fixture;

        public QueryableBatchGeneratorIntegrationTest(IntegrationTestFixture fixture)
        {
            this._fixture = fixture;
        }

        [Fact]
        public void ShouldGenerateListOfQueryablesForGivenQueryable()
        {
            // Arrange
            // Arrange
            using (var context = new MyDbContext(_fixture.DbContextOptions))
            {
                // Prepare the test data
                int count = 24;
                InsertTestData(context, count);

                // Create the queryable
                var query = context.Users.AsQueryable();

                // Act
                int batchSize = 10;
                var queryableBatch = QueryableBatchGenerator.Generate(query, batchSize);

                // Assert
                queryableBatch.Should().HaveCount(3);
                queryableBatch[0].ToList().Should().HaveCount(10);
                queryableBatch[1].ToList().Should().HaveCount(10);
                queryableBatch[2].ToList().Should().HaveCount(4);
            }
        }

        private void InsertTestData(MyDbContext context, int count)
        {
            var faker = new Faker<User>()
                .RuleFor(p => p.Id, f => Guid.NewGuid())
                .RuleFor(p => p.Name, f => f.Name.FullName())
                .RuleFor(p => p.Age, f => f.Random.Number(18, 65))
                .RuleFor(p => p.Email, (f, p) => f.Internet.Email(p.Name));

            var persons = faker.Generate(count);

            // Insert the test data into the database using EF
            context.Users.AddRange(persons);
            context.SaveChanges();
        }
    }
}
