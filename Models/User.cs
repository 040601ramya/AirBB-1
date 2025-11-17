using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AirBB.Models
{
    public class User : IValidatableObject
    {
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; } = "";

        [Phone]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string SSN { get; set; } = "";

        [Required]
        public string UserType { get; set; } = "";

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(PhoneNumber) && string.IsNullOrWhiteSpace(Email))
            {
                yield return new ValidationResult(
                    "Either phone number or email is required.",
                    new[] { nameof(PhoneNumber), nameof(Email) }
                );
            }
        }
    }
}
