using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryLogisticsAndTracking.Models
{
    /*<summary>
    Represents a user in the logistics system.
    Each user can only have **one** associated password.
    </summary>*/
    [Table("Users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "User type is required.")]
        [MaxLength(50)]
        public string UserType { get; set; } = string.Empty;

        [Column("DateOfBirth")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(User), nameof(ValidateDOB))]
        public DateTime DOB { get; set; }

        // Validates that the email matches standard email format.
        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(100)]
        [EmailAddress(ErrorMessage = "Email must be in a valid format (example@domain.com)")]
        public string Email { get; set; } = string.Empty;

        // Validates that the phone number contains only digits and is between 8 to 12 characters long.
        [Required(ErrorMessage = "Phone number is required.")]
        [Column("PhoneNumber")]
        [RegularExpression(@"^\d{8,12}$", ErrorMessage = "Phone number must be 8 to 12 digits and contain only numbers")]
        public string Phone { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Address { get; set; }

        // Navigation property to the user's login credential.
        // Each user has exactly **one** UserAndPass entry.
        public UserAndPass UserAndPass { get; set; } = null!;

        // Validates that the date of birth is not in the future.
        public static ValidationResult? ValidateDOB(DateTime dob, ValidationContext context)
        {
            if (dob > DateTime.Today)
                return new ValidationResult("Date of birth cannot be in the future.");

            return ValidationResult.Success;
        }
    }
}