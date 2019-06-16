using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class MovieReservation
    {
        public int ReservationId { get; set; }
        public int MovieId { get; set; }
        public Reservation Reservation { get; set; }
        public Movie Movie { get; set; }
    }
}
