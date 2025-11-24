using System;
using AirBB.Models;

namespace AirBB.Models.DomainModels
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        public DateTime ReservationStartDate { get; set; }
        public DateTime ReservationEndDate { get; set; }

       
        public int ClientId { get; set; }  
        public Client? Client { get; set; }

        public int ResidenceId { get; set; }
        public Residence? Residence { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
