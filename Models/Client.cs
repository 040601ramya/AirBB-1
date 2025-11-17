using System;
using System.ComponentModel.DataAnnotations;

namespace AirBB.Models
{
    public class Client
    {
        public int ClientId { get; set; }   // ✅ FIXED PRIMARY KEY

        [Required]
        public string Name { get; set; } = "";

        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }

        public DateTime DOB { get; set; }
    }
}
