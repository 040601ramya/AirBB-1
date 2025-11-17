using System.ComponentModel.DataAnnotations;

namespace AirBB.Models.Validation
{
    /// <summary>
    /// Built year must be a past year within the last 150 years.
    /// </summary>
    public class BuiltYearValidationAttribute : ValidationAttribute
    {
        public BuiltYearValidationAttribute()
        {
            ErrorMessage = "Built year must be a past year within the last 150 years.";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (!int.TryParse(value.ToString(), out var year))
                return new ValidationResult(ErrorMessage);

            var currentYear = DateTime.Now.Year;
            if (year > currentYear || year < currentYear - 150)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
