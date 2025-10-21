using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryLogisticsAndTracking.Models
{
    /*<summary>
    Represents the login credential (password) for a user.
    Each UserAndPass entry belongs to exactly **one** User.
    </summary>*/
    [Table("UserAndPass")]
    public class UserAndPass
    {
        [Key]
        public int UserAndPassId { get; set; }

        [Required]
        [MaxLength(256)]
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Foreign key to the associated user.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Navigation property to the User.
        /// Each password belongs to exactly one user.
        /// </summary>
        public User User { get; set; } = null!;
    }
}