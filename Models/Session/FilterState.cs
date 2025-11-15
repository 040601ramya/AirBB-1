namespace AirBB.Models.Session
{
    public class FilterState
    {
        public int SelectedLocationId { get; set; }
        public int GuestCount { get; set; } = 1;
        public string? StartDateIso { get; set; }  // "yyyy-MM-dd"
        public string? EndDateIso { get; set; }    // "yyyy-MM-dd"
    }
}
