namespace DeliveryLogisticsAndTracking.Models
{
    public class Delivery
    {
        public int DeliveryId { get; set; }
        public DateTime DeliveryDateTime { get; set; }
        public string DeliverySource { get; set; } = string.Empty;
        public string DeliveryDestination { get; set; } = string.Empty;
        public decimal DeliveryCharge { get; set; }

        // Foreign keys
        public int DriverId { get; set; }
        public User Driver { get; set; } = null!;

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } = null!;

        public int ShipmentId { get; set; }
        public Shipment Shipment { get; set; } = null!;
    }
}