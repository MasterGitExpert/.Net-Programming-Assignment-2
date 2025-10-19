using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DeliveryLogisticsAndTracking;

namespace DeliveryLogisticsAndTracking.Data
{
    public class DeliveryLogisticsAndTrackingContext : DbContext
    {
        public DeliveryLogisticsAndTrackingContext (DbContextOptions<DeliveryLogisticsAndTrackingContext> options)
            : base(options)
        {
        }

        public DbSet<DeliveryLogisticsAndTracking.Role> Role { get; set; } = default!;
        public DbSet<DeliveryLogisticsAndTracking.User> User { get; set; } = default!;
        public DbSet<DeliveryLogisticsAndTracking.VehicleType> VehicleType { get; set; } = default!;
        public DbSet<DeliveryLogisticsAndTracking.Vehicle> Vehicle { get; set; } = default!;
        public DbSet<DeliveryLogisticsAndTracking.Vendor> Vendor { get; set; } = default!;
        public DbSet<DeliveryLogisticsAndTracking.Shipment> Shipment { get; set; } = default!;
        public DbSet<DeliveryLogisticsAndTracking.Delivery> Delivery { get; set; } = default!;
    }
}
