using System.ComponentModel.DataAnnotations;

namespace DeliveryLogisticsAndTracking
{
    public class User
    {
        public int UserId { get; set; }

        public string? Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public decimal Salary { get; set; }

        public string? Password { get; set; }

        public UserType? UserType { get; set; }

    }
}
