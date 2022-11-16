using EntityFrameworkRelations.DataModel;

namespace EntityFrameworkRelations
{
    public class UnitTest1
    {


        [Fact]
        public void InsertAndRetrieveAuthor()
        {
            using (var db = new BookStoreContext())
            {
                // Create and save a new Blog
                Console.Write("Enter a name for a new Blog: ");
                var name = Console.ReadLine();

                var authorId = Guid.NewGuid();
                var authorName = "name";
                var author = new Author { Name = authorName, AuthorId = authorId};
                db.Authors.Add(author);
                db.SaveChanges();

                // Display all Authors from the database
                Author authorFromDb = db.Authors.AsQueryable().FirsOrDefault(x => x.AuthorId == authorId);

                Assert.NotNull(authorFromDb);
                Assert.Equal(authorName, authorFromDb.Name);
            }
        }
    }
}