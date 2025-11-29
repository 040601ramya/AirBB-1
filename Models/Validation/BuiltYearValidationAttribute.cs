using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AirBB.Models.Validation
{
   
    public class BuiltYearValidationAttribute : ValidationAttribute, IClientModelValidator
    {
       
        public int MaxYearsAgo { get; set; } = 150;

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

            if (year > currentYear || year < currentYear - MaxYearsAgo)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }

     
        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-builtyear", ErrorMessage!);
            MergeAttribute(context.Attributes, "data-val-builtyear-maxyearsago", MaxYearsAgo.ToString());
        }

        private static bool MergeAttribute(
            IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }

            attributes.Add(key, value);
            return true;
        }
    }
}
