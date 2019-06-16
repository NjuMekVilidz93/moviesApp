using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTO
{
    public class DirectorDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required.")][RegularExpression(@"^[A-Z][a-z]+\s[A-Z][a-z]+$", ErrorMessage = "Name is not correct.")] //Must be in format FirstName + LastName
        public string Name { get; set; }
    }
}
