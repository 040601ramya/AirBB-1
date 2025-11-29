using System.ComponentModel.DataAnnotations;

namespace AirBB.Models.Validation
{
    public class BathroomValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            if (value == null)
                return new ValidationResult("Bathrooms is required.");

            if (!decimal.TryParse(value.ToString(), out decimal number))
                return new ValidationResult("Invalid value.");

          
            decimal multiplied = number * 2;

            if (multiplied != Math.Truncate(multiplied))
                return new ValidationResult("Bathrooms must be an integer or end with .5");

            return ValidationResult.Success;
        }
    }
}
