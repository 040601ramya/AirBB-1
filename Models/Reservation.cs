using System;

namespace AirBB.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }               // PK per spec
        public DateTime ReservationStartDate { get; set; }    // Check-in
        public DateTime ReservationEndDate { get; set; }      // Check-out

        public int ResidenceId { get; set; }
        public Residence? Residence { get; set; }

        public int ClientId { get; set; }
        public Client? Client { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
