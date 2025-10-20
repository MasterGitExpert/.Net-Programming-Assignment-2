using Radzen;

namespace DeliveryLogisticsAndTracking.Components.Models
{
    public class Package
    {
        public string Name { get; set; } = string.Empty;
        public GoogleMapPosition Destination { get; set; } = new();
        public string Address { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending"; // e.g., Pending, In Transit, Delivered
        public DateTime? EstimatedDeliveryTime { get; set; }
    }

}
