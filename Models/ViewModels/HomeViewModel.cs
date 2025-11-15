using System;
using System.Collections.Generic;

namespace AirBB.Models.ViewModels
{
    public class HomeViewModel
    {
        public int SelectedLocationId { get; set; }  // 0 = All
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int GuestCount { get; set; } = 1;

        public List<Location> Locations { get; set; } = new();
        public List<Residence> Residences { get; set; } = new();
    }
}
