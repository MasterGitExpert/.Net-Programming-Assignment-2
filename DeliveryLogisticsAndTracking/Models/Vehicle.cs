namespace DeliveryLogisticsAndTracking.Models
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public string RegistrationNumber { get; set; } = string.Empty;
        public decimal PayloadCapacity { get; set; }
        public decimal VehicleCost { get; set; }

        // Foreign key
        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; } = null!;

        // Navigation property (optional)
        public ICollection<Delivery>? Deliveries { get; set; }
    }
}