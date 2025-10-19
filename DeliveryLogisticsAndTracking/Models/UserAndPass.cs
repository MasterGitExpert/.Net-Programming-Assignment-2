using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryLogisticsAndTracking.Models
{
    [Table("UserAndPass")]
    public class UserAndPass
    {
        [Key]
        public int UserAndPassId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(256)]
        public string PasswordHash { get; set; } = string.Empty;

        // Navigation property
        public User? User { get; set; }
    }
}