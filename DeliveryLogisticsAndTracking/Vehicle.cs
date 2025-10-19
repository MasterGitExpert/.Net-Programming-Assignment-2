namespace DeliveryLogisticsAndTracking
{
    public class Vehicle
    {
        public int VehicleId { get; set; }

        public string? RegistrationNumber { get; set; }

        public decimal PayloadCapacity { get; set; }

        public decimal VehicleCost { get; set; }

        public VehicleType? VehicleType { get; set; }

    }
}
