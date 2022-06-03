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
            modelBuilder.Entity<Dream>()
                .HasOne(p => p.UserAccount)
                .WithMany(p => p.Dreams)
                .OnDelete(DeleteBehavior.Cascade);
            //modelBuilder.Entity<DreamPublication>().ToTable("dream_publications");

            base.OnModelCreating(modelBuilder);
        }
    }
}
