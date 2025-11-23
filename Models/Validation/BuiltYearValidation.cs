using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AirBB.Models.Validation
{
    // Now supports both server-side and client-side validation
    public class BuiltYearValidation : ValidationAttribute, IClientModelValidator
    {
        // How many years back are allowed (default 150)
        public int MaxYearsAgo { get; set; } = 150;

        public BuiltYearValidation()
        {
            ErrorMessage = "Built year must be a past year within the last 150 years.";
        }

        // ✅ SERVER-SIDE VALIDATION
        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            if (value == null)
                return new ValidationResult("Built year is required.");

            if (!int.TryParse(value.ToString(), out int year))
                return new ValidationResult("Invalid year.");

            int current = DateTime.Now.Year;

            if (year > current)
                return new ValidationResult("Built year must be in the past.");

            if (year < current - MaxYearsAgo)
                return new ValidationResult($"Built year cannot be more than {MaxYearsAgo} years ago.");

            return ValidationResult.Success;
        }

        // ✅ CLIENT-SIDE VALIDATION (unobtrusive)
        public void AddValidation(ClientModelValidationContext context)
        {
            // Turn validation on
            MergeAttribute(context.Attributes, "data-val", "true");

            // Main error message
            MergeAttribute(context.Attributes, "data-val-builtyear", ErrorMessage!);

            // Parameter for JS side
            MergeAttribute(context.Attributes, "data-val-builtyear-maxyearsago", MaxYearsAgo.ToString());
        }

        private static bool MergeAttribute(
            IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
                return false;

            attributes.Add(key, value);
            return true;
        }
    }
}
