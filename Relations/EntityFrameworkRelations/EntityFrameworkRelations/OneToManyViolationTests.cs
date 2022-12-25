using EntityFrameworkRelations.Context;
using EntityFrameworkRelations.DataModel;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkRelations
{
    public class OneToManyViolationTests
    {

        [Fact]
        public void AddingNewCreatedInstanceToContext_WithSamePrimaryKeyOfAlreadyTrackedEntity_ThrowsExceptionBecauseCollisionWithAlreadyTracked()
        {
            using (var db = new BookStoreDbContext())
            {
                var bookId = Guid.NewGuid();
                var bookName = $"BookName_{bookId.ToString()}";
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

                var repeatedBook = new Book { Name = $"{bookName}_Repeated", BookId = bookId };
                authorFromDb.Books.Add(repeatedBook);

                Action action = () => db.SaveChanges();

                var ex = Assert.Throws<InvalidOperationException>(action);
                // ex.Message:
                // The instance of entity type 'Book' cannot be tracked because another instance with the same key value for {'BookId'} is already being tracked.
                // When attaching existing entities, ensure that only one entity instance with a given key value is attached.
                // Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see the conflicting key values.
            }

        }

        [Fact]
        public void AddingNewCreatedInstanceToContext_WithExistingPrimaryKeyInDb_ThrowsExceptionBecauseSQLViolationOfPrimaryKey()
        {
            var bookId = Guid.NewGuid();
            var bookName = $"BookName_{bookId.ToString()}";
            var authorId = Guid.NewGuid();
            var authorName = $"AuthorName_{authorId.ToString()}";

            using (var db = new BookStoreDbContext())
            {
                var book = new Book { Name = bookName, BookId = bookId };
                var author = new Author { Name = authorName, AuthorId = authorId };
                author.Books.Add(book);
                db.Authors.Add(author);

                db.SaveChanges();
            }

            using (var db = new BookStoreDbContext())
            {
                var repeatedBook = new Book { Name = $"{bookName}_Repeated", BookId = bookId };
                db.Books.Add(repeatedBook);

                Action action = () => db.SaveChanges();

                var ex = Assert.Throws<Microsoft.EntityFrameworkCore.DbUpdateException>(action);

                //ex.Message
                //An error occurred while saving the entity changes. See the inner exception for details.

                // ex.InnerException.Message
                // Violation of PRIMARY KEY constraint 'PK_Books'.Cannot insert duplicate key in object 'dbo.Books'.The duplicate key value is (5f6888bf - 97e2 - 49b6 - 9eb3 - 4de84429f2d0).
                // The statement has been terminated.
            }
        }

        [Fact]
        public void UsingNewCreatedInstanceInsteadOfTrackedExistingOne_WhenChangingFather_ThrowsExceptionBecauseSQLViolationOfPrimaryKey()
        {
            var bookId = Guid.NewGuid();
            var bookName = $"BookName_{bookId.ToString()}";
            var authorId = Guid.NewGuid();
            var authorName = $"AuthorName_{authorId.ToString()}";

            using (var db = new BookStoreDbContext())
            {
                var book = new Book { Name = bookName, BookId = bookId };
                var author = new Author { Name = authorName, AuthorId = authorId };
                author.Books.Add(book);
                db.Authors.Add(author);

                db.SaveChanges();
            }

            using (var db = new BookStoreDbContext())
            {
                var author2Id = Guid.NewGuid();
                var author2Name = $"Author2Name_{authorId.ToString()}";
                var author2 = new Author { Name = author2Name, AuthorId = author2Id };

                var repeatedBook = new Book { Name = $"{bookName}_Repeated", BookId = bookId };
                author2.Books.Add(repeatedBook);

                db.Authors.Add(author2);

                Action action = () => db.SaveChanges();

                var ex = Assert.Throws<Microsoft.EntityFrameworkCore.DbUpdateException>(action);

                //ex.Message
                //An error occurred while saving the entity changes. See the inner exception for details.

                // ex.InnerException.Message
                // Violation of PRIMARY KEY constraint 'PK_Books'.Cannot insert duplicate key in object 'dbo.Books'.The duplicate key value is (5f6888bf - 97e2 - 49b6 - 9eb3 - 4de84429f2d0).
                // The statement has been terminated.
            }
        }

    }
}