using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DreamWeb.Models
{
    public class DreamsContext : IdentityDbContext<UserAccount>
    {
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<DreamPublication> DreamPublications { get; set; }

        public DreamsContext(DbContextOptions<DreamsContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccount>().ToTable("dreams_user");
            modelBuilder.Entity<DreamPublication>().ToTable("dream_publications");

            base.OnModelCreating(modelBuilder);
        }
    }
}
