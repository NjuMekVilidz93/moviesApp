using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Movie : BaseEntity
    {
        public string Title { get; set; }
        public int? DirectorId { get; set; }
        public Director Director { get; set; }
        public string Description { get; set; }
        public int AvailableCount { get; set; } //koliko filomva ima na stanju
        public int Year { get; set; }
        public int Count { get; set; } //ukupan broj filmova
        public ICollection<MovieGenre> MovieGenres { get; set; }
        public ICollection<MovieReservation> ReservationMovies { get; set; }

    }
}
