using Intellimen.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Intellimen.Repository.Context
{
    public class IntellimenDbContext : DbContext
    {
        public IntellimenDbContext(DbContextOptions<IntellimenDbContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Profile> Profile { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<Challenge> Challenge { get; set; }
        public DbSet<ChallengeUser> ChallengeUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
