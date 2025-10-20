namespace DeliveryLogisticsAndTracking.Models
{
    public class Vendor
    {
        public int VendorId { get; set; }
        public string VendorName { get; set; } = string.Empty;

        // Navigation property (optional)
        public ICollection<Shipment>? Shipments { get; set; }
    }
}