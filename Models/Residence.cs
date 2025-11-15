namespace AirBB.Models
{
    public class Residence
    {
        public int ResidenceId { get; set; }
        public string Name { get; set; } = "";
        public string ResidencePicture { get; set; } = "";
        public int LocationId { get; set; }           // <- the ONLY FK column
        public Location? Location { get; set; }       // <- nav property
        public int GuestNumber { get; set; }
        public int BedroomNumber { get; set; }
        public int BathroomNumber { get; set; }
        public decimal PricePerNight { get; set; }
    }
}
