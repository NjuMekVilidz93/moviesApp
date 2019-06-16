using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required.")][RegularExpression("^[A-Z]{1}[a-z]{2,15}$",ErrorMessage = "FirstName is not correct.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [RegularExpression("^[A-Z]{1}[a-z]{2,15}$", ErrorMessage = "LastName is not correct.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [RegularExpression("^[a-z0-9_-]{3,16}$", ErrorMessage = "Username is not correct.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [MinLength(8,ErrorMessage = "Password must have at least 8 characters.")]
        public string Password { get; set; }
        public string Role { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int RoleId { get; set; }
    }
}
