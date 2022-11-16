using EntityFrameworkRelations.Context;
using EntityFrameworkRelations.DataModel;
using Microsoft.EntityFrameworkCore;

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


        [Fact]
        public void InsertAuthorWithRelatedBook()
        {
            using (var db = new BookStoreDbContext())
            {
                var bookId = Guid.NewGuid();
                var bookName = $"AuthorName_{bookId.ToString()}";
                var book = new Book { Name = bookName, BookId = bookId };

                var authorId = Guid.NewGuid();
                var authorName = $"AuthorName_{authorId.ToString()}";
                var author = new Author { Name = authorName, AuthorId = authorId };

                author.Books.Add(book);
                db.Authors.Add(author);

                db.SaveChanges();

                // Display all Authors from the database
                Author authorFromDb = db.Authors
                    .Include(x => x.Books)
                    .AsQueryable()
                    .FirstOrDefault(x => x.AuthorId == authorId);

                Assert.NotNull(authorFromDb);
                Assert.Equal(authorName, authorFromDb?.Name);
                Assert.Equal(1, authorFromDb.Books.Count);
            }
        }

        [Fact]
        public void AddingABook_WhenBookIdAlreadyExisting_ShouldThrowException()
        {
            using (var db = new BookStoreDbContext())
            {
                var bookId = Guid.NewGuid();
                var bookName = $"AuthorName_{bookId.ToString()}";
                var book = new Book { Name = bookName, BookId = bookId };

                var authorId = Guid.NewGuid();
                var authorName = $"AuthorName_{authorId.ToString()}";
                var author = new Author { Name = authorName, AuthorId = authorId };

                author.Books.Add(book);
                db.Authors.Add(author);

                db.SaveChanges();

                // Display all Authors from the database
                Author authorFromDb = db.Authors
                    .Include(x => x.Books)
                    .AsQueryable()
                    .FirstOrDefault(x => x.AuthorId == authorId);

                var repeatedBook = new Book { Name = $"{ bookName }_Repeated", BookId = bookId };
                authorFromDb.Books.Add(repeatedBook);

                Action action = () => db.SaveChanges();

                var ex = Assert.Throws<InvalidOperationException>(action);
                // ex.Message:
                // The instance of entity type 'Book' cannot be tracked because another instance with the same key value for {'BookId'} is already being tracked.
                // When attaching existing entities, ensure that only one entity instance with a given key value is attached.
                // Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see the conflicting key values.


            }
        }
    }
}