using System.ComponentModel.DataAnnotations;

namespace AirBB.Models.Validation
{
    public class BuiltYearValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            if (value == null)
                return new ValidationResult("Built year is required.");

            int year;
            if (!int.TryParse(value.ToString(), out year))
                return new ValidationResult("Invalid year.");

            int current = DateTime.Now.Year;

            if (year > current)
                return new ValidationResult("Built year must be in the past.");

            if (year < current - 150)
                return new ValidationResult($"Built year cannot be more than 150 years ago.");

            return ValidationResult.Success;
        }
    }
}
