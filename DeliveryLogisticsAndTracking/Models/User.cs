using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryLogisticsAndTracking.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "User type is required.")]
        public string UserType { get; set; } = string.Empty;

        [Column("DateOfBirth")]
        [CustomValidation(typeof(User), nameof(ValidateDOB))]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Email must be in a valid format (example@domain.com)")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required.")]
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
