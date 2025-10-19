using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryLogisticsAndTracking
{
    public class Vehicle
    {
        public int VehicleId { get; set; }

        public string? RegistrationNumber { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PayloadCapacity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal VehicleCost { get; set; }

        [Required]
        [ForeignKey("VehicleType")]
        public int VehicleTypeId { get; set; }

        public VehicleType? VehicleType { get; set; }

    }
}
