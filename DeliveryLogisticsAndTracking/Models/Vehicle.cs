namespace DeliveryLogisticsAndTracking.Models
{
    /*<summary>
    Represents a Vehicle in the logistics system.
    Each vehicle can only have **one** associated Vehicle Type.
    </summary>*/
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