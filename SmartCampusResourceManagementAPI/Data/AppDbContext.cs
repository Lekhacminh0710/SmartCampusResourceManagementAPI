using Microsoft.EntityFrameworkCore;
using SmartCampusResourceManagementAPI.Models;

namespace SmartCampusResourceManagementAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ResourceCategory> Categories => Set<ResourceCategory>();
        public DbSet<LearningResource> Resources => Set<LearningResource>();
        public DbSet<UserAccount> Users => Set<UserAccount>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ResourceCategory>().HasKey(c => c.CategoryId);
            modelBuilder.Entity<LearningResource>().HasKey(r => r.ResourceId);
            modelBuilder.Entity<UserAccount>().HasKey(u => u.UserId);

            modelBuilder.Entity<ResourceCategory>()
                .HasMany(c => c.LearningResources)
                .WithOne(r => r.Category)
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserAccount>()
                .HasMany(u => u.CreatedResources)
                .WithOne(r => r.CreatedBy)
                .HasForeignKey(r => r.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserAccount>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
