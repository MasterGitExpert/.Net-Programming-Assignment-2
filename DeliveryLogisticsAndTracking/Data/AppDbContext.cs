using DeliveryLogisticsAndTracking.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliveryLogisticsAndTracking.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserAndPass> UserAndPasses { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Map User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.UserType).IsRequired().HasMaxLength(50);
                entity.Property(e => e.DOB).HasColumnName("DateOfBirth");
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Phone).HasColumnName("PhoneNumber").HasMaxLength(20);
                entity.Property(e => e.Address).HasMaxLength(200);

                // Navigation property collection
                entity.HasMany(u => u.UserAndPasses)
                      .WithOne(uap => uap.User)
                      .HasForeignKey(uap => uap.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Map UserAndPass entity
            modelBuilder.Entity<UserAndPass>(entity =>
            {
                entity.ToTable("UserAndPass");
                entity.HasKey(e => e.UserAndPassId);
                entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(256);
            });
        }
    }
}