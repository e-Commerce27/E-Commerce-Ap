using System.ComponentModel.DataAnnotations;

namespace E_Commerce_App.Models.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        // BAD! You'd never really let a user specify their role!
        public List<string> Roles { get; set; }
    }
}
