using Application.DTO;
using Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class MovieViewModel
    {
        public IEnumerable<MovieDto> Movies { get; set; }
    }
}
