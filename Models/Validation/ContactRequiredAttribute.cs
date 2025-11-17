using System.ComponentModel.DataAnnotations;

namespace AirBB.Models.Validation
{
    public class ContactRequiredAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext ctx)
        {
            var user = (AirBB.Models.User)ctx.ObjectInstance;

            bool hasEmail = !string.IsNullOrWhiteSpace(user.Email);
            bool hasPhone = !string.IsNullOrWhiteSpace(user.PhoneNumber);

            if (!hasEmail && !hasPhone)
            {
                return new ValidationResult("Either Email or Phone Number must be provided.");
            }

            return ValidationResult.Success;
        }
    }
}
