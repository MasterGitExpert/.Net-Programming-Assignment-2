namespace DeliveryLogisticsAndTracking.Models
{
    public class Shipment
    {
        public int ShipmentId { get; set; }
        public decimal ShipmentWeight { get; set; }
        public string ShipmentPriority { get; set; } = string.Empty;
        public string ShipmentItem { get; set; } = string.Empty;

        // Foreign key
        public int VendorId { get; set; }
        public Vendor Vendor { get; set; } = null!;

        // Navigation property
        public ICollection<Delivery>? Deliveries { get; set; }
    }
}