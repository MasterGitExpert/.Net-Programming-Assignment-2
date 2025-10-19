using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace DeliveryLogisticsAndTracking.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string UserType { get; set; } = string.Empty;

        [Column("DateOfBirth")]
        [CustomValidation(typeof(User), nameof(ValidateDOB))]
        public DateTime DOB { get; set; }

        [Required]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Email must be in a valid format (example@domain.com)")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Column("PhoneNumber")]
        [RegularExpression(@"^\d{8,12}$", ErrorMessage = "Phone number must be 8 to 12 digits and contain only numbers")]
        public string Phone { get; set; } = string.Empty;

        public string? Address { get; set; }

        public ICollection<UserAndPass> UserAndPasses { get; set; } = new List<UserAndPass>();

        public static ValidationResult? ValidateDOB(DateTime dob, ValidationContext context)
        {
            if (dob > DateTime.Today)
            {
                return new ValidationResult("Date of birth cannot be in the future.");
            }
            return ValidationResult.Success;
        }
    }
}