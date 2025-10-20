namespace DeliveryLogisticsAndTracking.Models
{
    /*<summary>
    Represents a  in Vendor in the logistics system.
    Each user can have **many** associated shipments.
    </summary>*/
    public class Vendor
    {
        public int VendorId { get; set; }
        public string VendorName { get; set; } = string.Empty;

        // Navigation property (optional)
        public ICollection<Shipment>? Shipments { get; set; }
    }
}