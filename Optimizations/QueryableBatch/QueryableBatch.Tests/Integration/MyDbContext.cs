using QueryableBatch.Tests.Unit;
using Microsoft.EntityFrameworkCore;

namespace QueryableBatch.Tests.Integration
{
    public class MyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
    }
}
