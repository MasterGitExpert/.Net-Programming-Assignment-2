namespace DeliveryLogisticsAndTracking.Models
{
    /*<summary>
    Represents a Vehicle Type in the logistics system.
    Each Vehicle type can belong to **many** associated Vehicles.
    </summary>*/
    public class VehicleType
    {
        public int VehicleTypeId { get; set; }
        public string VehicleTypeName { get; set; } = string.Empty;

        // Navigation property (optional)
        public ICollection<Vehicle>? Vehicles { get; set; }
    }
}