using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Column(TypeName = "decimal(18,4)")]
        public decimal Salary { get; set; }

        public string? Password { get; set; }

        public int? RoleId { get; set; }
        public Role? Role { get; set; }

    }
}
