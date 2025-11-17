using System;
using AirBB.Models;

namespace AirBB.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        public DateTime ReservationStartDate { get; set; }
        public DateTime ReservationEndDate { get; set; }

        // The foreign key must match what AirBnbContext expects
        public int ClientId { get; set; }  
        public Client? Client { get; set; }

        public int ResidenceId { get; set; }
        public Residence? Residence { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
