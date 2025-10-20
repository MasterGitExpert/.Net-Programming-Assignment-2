using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryLogisticsAndTracking
{
    public class Delivery
    {
        public int DeliveryId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DeliveryDateTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ETADateTime { get; set; }

        public string? Source { get; set; }

        public string? Destination { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DeliveryCharge { get; set; }

        [Required]
        [ForeignKey("Driver")]
        public int DriverId { get; set; }

        public virtual User Driver { get; set; }

        [Required]
        [ForeignKey("DeliveryVehicle")]
        public int DeliveryVehicleId { get; set; }

        public virtual Vehicle DeliveryVehicle { get; set; }

        [Required]
        [ForeignKey("GoodsShipment")]
        public int GoodsShipmentId { get; set; }

        public virtual Shipment GoodsShipment { get; set; }

    }
}
