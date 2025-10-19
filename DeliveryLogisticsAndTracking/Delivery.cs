using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryLogisticsAndTracking
{
    public class Delivery
    {
        public int DeliveryId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DeliveryDateTime { get; set; }

        public string? Source { get; set; }

        public string? Destination { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal DeliveryCharge { get; set; }

        public int? DriverId { get; set; }

        public User? Driver { get; set; }

        public int? DeliveryVehicleId { get; set; }

        public Vehicle? DeliveryVehicle { get; set; }

        public int? GoodsShipmentId { get; set; }

        public Shipment? GoodsShipment { get; set; }

    }
}
