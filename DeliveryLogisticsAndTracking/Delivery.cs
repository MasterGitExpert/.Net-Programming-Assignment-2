using System.ComponentModel.DataAnnotations;

namespace DeliveryLogisticsAndTracking
{
    public class Delivery
    {
        public int DeliveryId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DeliveryDateTime { get; set; }

        public string? Source { get; set; }

        public string? Destination { get; set; }

        public decimal DeliveryCharge { get; set; }

        public User? Driver { get; set; }

        public Vehicle? DeliveryVehicle { get; set; }

        public Shipment? GoodsShipment { get; set; }

    }
}
