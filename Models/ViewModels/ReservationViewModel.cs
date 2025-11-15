using System;

namespace AirBB.Models.ViewModels
{
    public class ReservationViewModel
    {
        public int ReservationId { get; set; }
        public string ResidenceName { get; set; } = "";
        public string LocationName { get; set; } = "";
        public DateTime ReservationStartDate { get; set; }
        public DateTime ReservationEndDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
