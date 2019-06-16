using Application.DTO;
using Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class CatalogViewModel
    {
        public PagedResponse<MovieDto> Movies { get; set; }
        public IEnumerable<GenreDto> Genres { get; set; }
    }
}
