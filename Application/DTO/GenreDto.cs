using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTO
{
    public class GenreDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [StringLength(50, ErrorMessage = "Name is not correct.")]
        public string Name { get; set; }
        public IEnumerable<int> Movies { get; set; }
    }
}
