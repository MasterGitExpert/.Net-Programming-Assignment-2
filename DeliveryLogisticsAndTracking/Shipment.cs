namespace DeliveryLogisticsAndTracking
{
    public class Shipment
    {
        public int ShipmentId { get; set; }

        public string? ShipmentName { get; set; }

        public decimal Weight { get; set; }

        public int Priority { get; set; }

        public Vendor? Vendor { get; set; }

    }
}
