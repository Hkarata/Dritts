using Dritts.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Dritts.Api.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserAccount> UsersAccount { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => new { u.FirstName, u.MiddleName, u.LastName })
                .IsUnique();

        }
    }
}
