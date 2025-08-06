using Microsoft.EntityFrameworkCore;

namespace Intellimen.Repository.Context
{
    public class IntellimenDbContext : DbContext
    {
        public IntellimenDbContext(DbContextOptions<IntellimenDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
