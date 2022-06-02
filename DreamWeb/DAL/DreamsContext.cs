using DreamWeb.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DreamWeb.DAL
{
    public class DreamsContext : IdentityDbContext<UserAccount>
    {
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Dream> DreamPublications { get; set; }

        public DreamsContext(DbContextOptions<DreamsContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccount>()
                .HasMany(p => p.Dreams)
                .WithOne(p => p.UserAccount)
                .HasForeignKey(p => p.Id)
                .OnDelete(DeleteBehavior.Cascade);
            //modelBuilder.Entity<DreamPublication>().ToTable("dream_publications");

            base.OnModelCreating(modelBuilder);
        }
    }
}
