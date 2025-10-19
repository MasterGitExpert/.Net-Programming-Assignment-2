using DeliveryLogisticsAndTracking.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliveryLogisticsAndTracking.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Tables
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserAndPass> UserAndPasses { get; set; } = null!;
        public DbSet<VehicleType> VehicleTypes { get; set; } = null!;
        public DbSet<Vehicle> Vehicles { get; set; } = null!;
        public DbSet<Vendor> Vendors { get; set; } = null!;
        public DbSet<Shipment> Shipments { get; set; } = null!;
        public DbSet<Delivery> Deliveries { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --------------------------
            // USER
            // --------------------------
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.DOB).HasColumnName("DateOfBirth");
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.Address).HasMaxLength(200);
                entity.Property(e => e.UserType).HasMaxLength(50);

                entity.HasMany(e => e.UserAndPasses)
                      .WithOne(up => up.User!)
                      .HasForeignKey(up => up.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // --------------------------
            // USER AND PASS
            // --------------------------
            modelBuilder.Entity<UserAndPass>(entity =>
            {
                entity.ToTable("UserAndPass");
                entity.HasKey(e => e.UserAndPassId);
                entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(256);
            });

            // --------------------------
            // VEHICLE TYPE
            // --------------------------
            modelBuilder.Entity<VehicleType>(entity =>
            {
                entity.ToTable("VehicleTypes");
                entity.HasKey(e => e.VehicleTypeId);
                entity.Property(e => e.VehicleTypeName).IsRequired().HasMaxLength(100);
            });

            // --------------------------
            // VEHICLE
            // --------------------------
            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.ToTable("Vehicles");
                entity.HasKey(e => e.VehicleId);

                entity.Property(e => e.RegistrationNumber).IsRequired().HasMaxLength(50);
                entity.Property(e => e.PayloadCapacity).HasColumnType("decimal(10,2)");
                entity.Property(e => e.VehicleCost).HasColumnType("decimal(10,2)");

                entity.HasOne(e => e.VehicleType)
                      .WithMany(vt => vt.Vehicles!)
                      .HasForeignKey(e => e.VehicleTypeId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // --------------------------
            // VENDOR
            // --------------------------
            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.ToTable("Vendors");
                entity.HasKey(e => e.VendorId);
                entity.Property(e => e.VendorName).IsRequired().HasMaxLength(100);
            });

            // --------------------------
            // SHIPMENT
            // --------------------------
            modelBuilder.Entity<Shipment>(entity =>
            {
                entity.ToTable("Shipments");
                entity.HasKey(e => e.ShipmentId);

                entity.Property(e => e.ShipmentWeight).HasColumnType("decimal(10,2)");
                entity.Property(e => e.ShipmentPriority).IsRequired().HasMaxLength(50);
                entity.Property(e => e.ShipmentItem).IsRequired().HasMaxLength(100);

                entity.HasOne(e => e.Vendor)
                      .WithMany(v => v.Shipments!)
                      .HasForeignKey(e => e.VendorId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // --------------------------
            // DELIVERY
            // --------------------------
            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.ToTable("Deliveries");
                entity.HasKey(e => e.DeliveryId);

                entity.Property(e => e.DeliveryDateTime).IsRequired();
                entity.Property(e => e.DeliverySource).IsRequired().HasMaxLength(100);
                entity.Property(e => e.DeliveryDestination).IsRequired().HasMaxLength(100);
                entity.Property(e => e.DeliveryCharge).HasColumnType("decimal(10,2)");

                entity.HasOne(e => e.Driver)
                      .WithMany()
                      .HasForeignKey(e => e.DriverId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Vehicle)
                      .WithMany(v => v.Deliveries!)
                      .HasForeignKey(e => e.VehicleId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Shipment)
                      .WithMany(s => s.Deliveries!)
                      .HasForeignKey(e => e.ShipmentId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}