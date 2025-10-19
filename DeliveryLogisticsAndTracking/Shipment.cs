using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryLogisticsAndTracking
{
    public class Shipment
    {
        public int ShipmentId { get; set; }

        public string? ShipmentName { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Weight { get; set; }

        public int Priority { get; set; }

        public bool Fragile { get; set; }

        [Required]
        [ForeignKey("Vendor")]
        public int VendorId { get; set; }

        public Vendor? Vendor { get; set; }

    }
}
