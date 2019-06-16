using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTO
{
    public class ReservationDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int UserId { get; set; }
        public string Username { get; set; }
        public DateTime? DateTaken { get; set; }
        public DateTime? DateToReturn { get; set; }
        public IEnumerable<int> MovieReservations { get; set; }
        public IEnumerable<string> Movies { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateExpiry { get; set; }
        public string MoviesSelected { get; set; }
    }
}
