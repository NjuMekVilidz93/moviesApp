using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Reservation : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime DateExpiry { get; set; }
        public DateTime DateTaken { get; set; }
        public DateTime DateToReturn { get; set; }
        public ICollection<MovieReservation> MovieReservations { get; set; }
    }
}
