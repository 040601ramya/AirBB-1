using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;         
using AirBB.Models.Validation;          
namespace AirBB.Models.DomainModels
{
    public class Residence
    {
        public int ResidenceId { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Name must be alphanumeric.")]
        public string Name { get; set; } = "";

        [Required]
        public string ResidencePicture { get; set; } = "";

        [Required]
        public int LocationId { get; set; }
        public Location? Location { get; set; }

        
        [Required]
        [Remote(
            action: "VerifyOwner",
            controller: "Residence",
            ErrorMessage = "OwnerId must exist in the Users table."
        )]
        public int OwnerId { get; set; }

        [Required]
        [Range(1, 50)]
        public int GuestNumber { get; set; }

        [Required]
        [Range(1, 20)]
        public int BedroomNumber { get; set; }

        [Required]
        public decimal BathroomNumber { get; set; }

  
        [Required]
        [BuiltYearValidationAttribute]
        public int BuiltYear { get; set; }

        [Required]
        [Range(10, 10000)]
        public decimal PricePerNight { get; set; }
    }
}
