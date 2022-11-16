using EntityFrameworkRelations.DataModel;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkRelations.Context
{
    public class BookStoreDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=BookStoreLab");
        }
    }
}
