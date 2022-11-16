using EntityFrameworkRelations.Context;
using EntityFrameworkRelations.DataModel;

namespace EntityFrameworkRelations
{
    public class UnitTest1
    {


        [Fact]
        public void InsertAndRetrieveAuthor()
        {
            using (var db = new BookStoreDbContext())
            {
                var authorId = Guid.NewGuid();
                var authorName = "name";
                var author = new Author { Name = authorName, AuthorId = authorId};
                db.Authors.Add(author);
                db.SaveChanges();

                // Display all Authors from the database
                Author authorFromDb = db.Authors.AsQueryable().FirstOrDefault(x => x.AuthorId == authorId);

                Assert.NotNull(authorFromDb);
                Assert.Equal(authorName, authorFromDb?.Name);
            }
        }
    }
}