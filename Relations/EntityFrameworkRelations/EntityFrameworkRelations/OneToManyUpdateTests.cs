using EntityFrameworkRelations.Context;
using EntityFrameworkRelations.DataModel;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkRelations
{
    public class OneToManyUpdateTests
    {

        [Fact]
        public void CreatingFatherWithChildren_SuccessfullyCreateFatherChildRelation()
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

                Assert.NotNull(authorFromDb);
                Assert.Equal(authorName, authorFromDb?.Name);
                Assert.Equal(1, authorFromDb.Books.Count);
            }
        }

        [Fact]
        public void ChangingFatherForeignKey_SuccessfullyChangesFather()
        {
            var bookId = Guid.NewGuid();
            var book1Name = $"Book1Name_{bookId.ToString()}";
            var author1Id = Guid.NewGuid();
            var author1Name = $"Author1Name_{author1Id.ToString()}";
            var author2Id = Guid.NewGuid();
            var author2Name = $"Author2Name_{author1Id.ToString()}";

            using (var db = new BookStoreDbContext())
            {
                var book = new Book { Name = book1Name, BookId = bookId };
                var author = new Author { Name = author1Name, AuthorId = author1Id };
                author.Books.Add(book);
                db.Authors.Add(author);

                var author2 = new Author { Name = author2Name, AuthorId = author2Id };
                db.Authors.Add(author2);

                db.SaveChanges();
            }

            using (var db = new BookStoreDbContext())
            {
                var authors = db.Authors.Where(x => x.AuthorId == author1Id || x.AuthorId == author2Id).ToList();
                var existingBook = db.Books.Include(x => x.Author).Single(x => x.BookId == bookId);

                existingBook.AuthorId = author2Id;

                db.SaveChanges();

                Assert.Equal(author2Id, existingBook.AuthorId);
                Assert.Equal(author2Id, existingBook.Author.AuthorId);
            }

        }

        [Fact]
        public void ChangingFatherNavigationProperty_SuccessfullyChangesFather()
        {
            var bookId = Guid.NewGuid();
            var book1Name = $"Book1Name_{bookId.ToString()}";
            var author1Id = Guid.NewGuid();
            var author1Name = $"Author1Name_{author1Id.ToString()}";
            var author2Id = Guid.NewGuid();
            var author2Name = $"Author2Name_{author1Id.ToString()}";

            using (var db = new BookStoreDbContext())
            {
                var book = new Book { Name = book1Name, BookId = bookId };
                var author = new Author { Name = author1Name, AuthorId = author1Id };
                author.Books.Add(book);
                db.Authors.Add(author);

                var author2 = new Author { Name = author2Name, AuthorId = author2Id };
                db.Authors.Add(author2);

                db.SaveChanges();
            }

            using (var db = new BookStoreDbContext())
            {
                var author2 = db.Authors.Where(x => x.AuthorId == author2Id).Single();
                var existingBook = db.Books.Include(x => x.Author).Single(x => x.BookId == bookId);

                existingBook.Author = author2;

                db.SaveChanges();

                Assert.Equal(author2Id, existingBook.AuthorId);
                Assert.Equal(author2Id, existingBook.Author.AuthorId);
            }
        }

        [Fact]
        public void ChangingFatherKeyAndNavigationProperty_SuccessfullyChangesFatherUsingNavigationProperty()
        {
            var bookId = Guid.NewGuid();
            var book1Name = $"Book1Name_{bookId.ToString()}";
            var author1Id = Guid.NewGuid();
            var author1Name = $"Author1Name_{author1Id.ToString()}";
            var author2Id = Guid.NewGuid();
            var author2Name = $"Author2Name_{author1Id.ToString()}";
            var author3Id = Guid.NewGuid();
            var author3Name = $"Author3Name_{author1Id.ToString()}";

            using (var db = new BookStoreDbContext())
            {
                var book = new Book { Name = book1Name, BookId = bookId };
                var author = new Author { Name = author1Name, AuthorId = author1Id };
                author.Books.Add(book);
                db.Authors.Add(author);

                var author2 = new Author { Name = author2Name, AuthorId = author2Id };
                db.Authors.Add(author2);
                var author3 = new Author { Name = author3Name, AuthorId = author3Id };
                db.Authors.Add(author3);

                db.SaveChanges();
            }

            using (var db = new BookStoreDbContext())
            {
                var author2 = db.Authors.Where(x => x.AuthorId == author2Id).Single();
                var existingBook = db.Books.Include(x => x.Author).Single(x => x.BookId == bookId);

                existingBook.Author = author2;
                existingBook.AuthorId = author3Id;

                db.SaveChanges();

                Assert.Equal(author2Id, existingBook.AuthorId);
                Assert.Equal(author2Id, existingBook.Author.AuthorId);
            }
        }

        [Fact]
        public void AddingChildToAnotherFatherChildrenList_SuccessfullyChangesFather()
        {
            var bookId = Guid.NewGuid();
            var book1Name = $"Book1Name_{bookId.ToString()}";
            var author1Id = Guid.NewGuid();
            var author1Name = $"Author1Name_{author1Id.ToString()}";
            var author2Id = Guid.NewGuid();
            var author2Name = $"Author2Name_{author1Id.ToString()}";

            using (var db = new BookStoreDbContext())
            {
                var book = new Book { Name = book1Name, BookId = bookId };
                var author = new Author { Name = author1Name, AuthorId = author1Id };
                author.Books.Add(book);
                db.Authors.Add(author);

                db.SaveChanges();
            }

            using (var db = new BookStoreDbContext())
            {


                var author2 = new Author { Name = author2Name, AuthorId = author2Id };

                var existingBook = db.Books.FirstOrDefault(x => x.BookId == bookId);

                Assert.Equal(author1Id, existingBook.AuthorId);

                author2.Books.Add(existingBook);
                db.Authors.Add(author2);
                db.SaveChanges();

                Assert.Equal(author2Id, existingBook.AuthorId);
            }
        }
    }
}