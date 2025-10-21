namespace DeliveryLogisticsAndTracking.Models
{
    /* <summary>
    Represents a delivery record in the system.
    A delivery is tied to a specific driver, vehicle, and shipment.
    <summary> */

    public class Delivery
    {
        public int DeliveryId { get; set; }

        public DateTime DeliveryDateTime { get; set; }

        public string DeliverySource { get; set; } = string.Empty;

        public string DeliveryDestination { get; set; } = string.Empty;

        public decimal DeliveryCharge { get; set; }


        // Foreign key to the driver (User) who performs the delivery.
        public int DriverId { get; set; }

        // Navigation property to the driver performing the delivery.
        public User Driver { get; set; } = null!;

        // Foreign key to the vehicle used for the delivery.
        public int VehicleId { get; set; }

        // Navigation property to the vehicle used in the delivery.
        public Vehicle Vehicle { get; set; } = null!;

        // Foreign key to the associated shipment being delivered.
        public int ShipmentId { get; set; }

        // Navigation property to the shipment associated with this delivery.
        public Shipment Shipment { get; set; } = null!;
    }
}