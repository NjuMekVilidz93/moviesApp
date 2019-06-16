using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches
{
    public class MovieSearch
    {
        public string MovieName { get; set; }
        public int? MovieYear { get; set; }
        public bool IsAvailable { get; set; }
        public int? GenreId { get; set; }
        public int PerPage { get; set; } = 5;
        public int PageNumber { get; set; } = 1;

    }
}
