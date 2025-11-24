using System.ComponentModel.DataAnnotations;

namespace AirBB.Models.DomainModels
{
    public class Location
    {
        public int LocationId { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; } = "";

        [Required]
        [StringLength(50)]
        public string State { get; set; } = "";

        [Required]
        [StringLength(50)]
        public string Country { get; set; } = "";
    }
}
