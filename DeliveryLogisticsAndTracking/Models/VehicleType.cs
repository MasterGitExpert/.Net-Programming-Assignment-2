namespace DeliveryLogisticsAndTracking.Models
{
    public class VehicleType
    {
        public int VehicleTypeId { get; set; }
        public string VehicleTypeName { get; set; } = string.Empty;

        // Navigation property (optional)
        public ICollection<Vehicle>? Vehicles { get; set; }
    }
}