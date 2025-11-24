using System;
using System.Collections.Generic;
using AirBB.Models.DomainModels;   // ✔ FIX: correct namespace for Residence & Location

namespace AirBB.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<Residence>? Residences { get; set; }
        public List<Location>? Locations { get; set; }

        public int SelectedLocationId { get; set; }
        public int GuestCount { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
