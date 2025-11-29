using System.ComponentModel.DataAnnotations;

namespace AirBB.Models.DomainModels
{
    public class Experience
    {
        public int ExperienceId { get; set; }

        [Required]
        public required string Title { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        [Range(0, 99999)]
        public decimal Price { get; set; }
    }
}
