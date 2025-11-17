using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace AirBB.Models.Validation
{
    /// <summary>
    /// Bathrooms: integer or .5 (e.g., 1, 1.5, 2, 2.5, etc.)
    /// </summary>
    public class BathroomValidationAttribute : ValidationAttribute
    {
        public BathroomValidationAttribute()
        {
            ErrorMessage = "Bathrooms must be an integer or end with .5 (e.g., 1, 1.5, 2).";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            var str = value.ToString();
            if (string.IsNullOrWhiteSpace(str))
                return ValidationResult.Success;

            if (!decimal.TryParse(str, NumberStyles.Number, CultureInfo.InvariantCulture, out var number))
                return new ValidationResult(ErrorMessage);

            // 0.5 steps
            var multiplied = number * 2;
            if (multiplied != Math.Truncate(multiplied) || number < 0)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
