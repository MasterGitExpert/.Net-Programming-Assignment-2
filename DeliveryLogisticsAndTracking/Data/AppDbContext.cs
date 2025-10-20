using DeliveryLogisticsAndTracking.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliveryLogisticsAndTracking.Data
{
    /* <summary>
    Application database context.
    Defines all tables and relationships for the .NET Logistics system.
    Inherits from Entity Framework Core's DbContext.
    </summary>*/
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // Database Tables (DbSets)
        
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserAndPass> UserAndPasses { get; set; } = null!;
        public DbSet<VehicleType> VehicleTypes { get; set; } = null!;
        public DbSet<Vehicle> Vehicles { get; set; } = null!;
        public DbSet<Vendor> Vendors { get; set; } = null!;
        public DbSet<Shipment> Shipments { get; set; } = null!;
        public DbSet<Delivery> Deliveries { get; set; } = null!;

        /* <summary>
        Configures entity mappings and relationships in the database.
        Called by EF Core when building the model.
        </summary>*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // USER

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.UserId);

                // Basic properties
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.DOB).HasColumnName("DateOfBirth");
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.Address).HasMaxLength(200);
                entity.Property(e => e.UserType).HasMaxLength(50);

                // One-to-one: User -> UserAndPass
                entity.HasOne(e => e.UserAndPass)
                      .WithOne(up => up.User!)
                      .HasForeignKey<UserAndPass>(up => up.UserId)
                      .OnDelete(DeleteBehavior.Cascade); // Deleting a user deletes their password
            });

            // USER AND PASSWORD

            modelBuilder.Entity<UserAndPass>(entity =>
            {
                entity.ToTable("UserAndPass");
                entity.HasKey(e => e.UserAndPassId);
                entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(256);
            });

            // VEHICLE TYPE

            modelBuilder.Entity<VehicleType>(entity =>
            {
                entity.ToTable("VehicleTypes");
                entity.HasKey(e => e.VehicleTypeId);
                entity.Property(e => e.VehicleTypeName).IsRequired().HasMaxLength(100);
            });


            // VEHICLE

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.ToTable("Vehicles");
                entity.HasKey(e => e.VehicleId);

                entity.Property(e => e.RegistrationNumber).IsRequired().HasMaxLength(50);
                entity.Property(e => e.PayloadCapacity).HasColumnType("decimal(10,2)");
                entity.Property(e => e.VehicleCost).HasColumnType("decimal(10,2)");

                // One-to-many: VehicleType -> Vehicles
                entity.HasOne(e => e.VehicleType)
                      .WithMany(vt => vt.Vehicles!)
                      .HasForeignKey(e => e.VehicleTypeId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // VENDOR

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.ToTable("Vendors");
                entity.HasKey(e => e.VendorId);
                entity.Property(e => e.VendorName).IsRequired().HasMaxLength(100);
            });


            // SHIPMENT

            modelBuilder.Entity<Shipment>(entity =>
            {
                entity.ToTable("Shipments");
                entity.HasKey(e => e.ShipmentId);

                entity.Property(e => e.ShipmentWeight).HasColumnType("decimal(10,2)");
                entity.Property(e => e.ShipmentPriority).IsRequired().HasMaxLength(50);
                entity.Property(e => e.ShipmentItem).IsRequired().HasMaxLength(100);

                // One-to-many: Vendor -> Shipments
                entity.HasOne(e => e.Vendor)
                      .WithMany(v => v.Shipments!)
                      .HasForeignKey(e => e.VendorId)
                      .OnDelete(DeleteBehavior.Cascade);
            });


            // DELIVERY

            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.ToTable("Deliveries");
                entity.HasKey(e => e.DeliveryId);

                entity.Property(e => e.DeliveryDateTime).IsRequired();
                entity.Property(e => e.DeliverySource).IsRequired().HasMaxLength(100);
                entity.Property(e => e.DeliveryDestination).IsRequired().HasMaxLength(100);
                entity.Property(e => e.DeliveryCharge).HasColumnType("decimal(10,2)");

                // Relationships:
                entity.HasOne(e => e.Driver) // Driver of delivery
                      .WithMany()
                      .HasForeignKey(e => e.DriverId)
                      .OnDelete(DeleteBehavior.Restrict); // Restrict deleting users that are drivers

                entity.HasOne(e => e.Vehicle) // Vehicle used
                      .WithMany(v => v.Deliveries!)
                      .HasForeignKey(e => e.VehicleId)
                      .OnDelete(DeleteBehavior.Restrict); // Vehicle cannot be deleted if deliveries exist

                entity.HasOne(e => e.Shipment) // Associated shipment
                      .WithMany(s => s.Deliveries!)
                      .HasForeignKey(e => e.ShipmentId)
                      .OnDelete(DeleteBehavior.Cascade); // Deleting shipment deletes its deliveries
            });
        }
    }
}