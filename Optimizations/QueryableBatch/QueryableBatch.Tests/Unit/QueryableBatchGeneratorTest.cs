using Bogus;
using FluentAssertions;

namespace QueryableBatch.Tests.Unit
{
    public class QueryableBatchGeneratorTest
    {
        [Fact]
        public void ShouldGenerateListOfQueryablesForGivenQueryable()
        {
            // Arrange
            var query = BuildDummyQueryableWithCount(24);

            // Act
            int batchSize = 10;
            var queryableBatch = QueryableBatchGenerator.Generate(query, batchSize);

            // Assert
            queryableBatch.Should().HaveCount(3);
            queryableBatch[0].ToList().Should().HaveCount(10);
            queryableBatch[1].ToList().Should().HaveCount(10);
            queryableBatch[2].ToList().Should().HaveCount(4);
        }

        private IQueryable<User> BuildDummyQueryableWithCount(int count)
        {
            var faker = new Faker<User>()
                .RuleFor(p => p.Name, f => f.Name.FullName())
                .RuleFor(p => p.Age, f => f.Random.Number(18, 65))
                .RuleFor(p => p.Email, (f, p) => f.Internet.Email(p.Name));

            var persons = faker.Generate(count);

            return persons.AsQueryable();
        }
    }

    internal class User
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Email { get; set; }
    }
}