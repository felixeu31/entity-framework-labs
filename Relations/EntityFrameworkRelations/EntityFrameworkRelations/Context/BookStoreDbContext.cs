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
            optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(p => p.Author)
                .WithMany(t => t.Books)
                .HasForeignKey(p => p.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
                //.OnDelete(DeleteBehavior.Cascade); //This line will cause cascade delete on navigation property removal (no orphan relation violation)
        }
    }
}
