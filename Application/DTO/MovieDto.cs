using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTO
{
    public class MovieDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [StringLength(50, ErrorMessage = "Name is not correct.")]
        public string Title { get; set; }
        public string Director { get; set; }
        public int? DirectorId { get; set; }
        public int? GenreId { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [StringLength(1000,ErrorMessage = "Description is not correct.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int AvailableCount { get; set; } //koliko filomva ima na stanju
        [Required(ErrorMessage = "This field is required.")]
        public int Year { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int Count { get; set; } //ukupan broj filmova
        public IEnumerable<string> MovieGenres { get; set; }
        public IEnumerable<int> SelectedGenres { get; set; }
        //public IEnumerable<string> ReservationMovies { get; set; }
        public string GenreName { get; set; }
    }
}
