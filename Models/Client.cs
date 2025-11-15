using System;
using System.ComponentModel.DataAnnotations;

namespace AirBB.Models
{
    public class Client
    {
        [Key]
        public int UserId { get; set; }     // Primary Key

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
    }
}