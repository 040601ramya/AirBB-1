using System.ComponentModel.DataAnnotations;

namespace AirBB.Models
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
        [Range(1, 50)]
        public int GuestNumber { get; set; }

        [Required]
        [Range(1, 20)]
        public int BedroomNumber { get; set; }

        [Required]
        public decimal BathroomNumber { get; set; }

        [Required]
        [Range(1800, 2050)]
        public int BuiltYear { get; set; }

        [Required]
        [Range(10, 10000)]
        public decimal PricePerNight { get; set; }
    }
}
