using System.ComponentModel.DataAnnotations;

namespace E_Commerce_App.Models.DTO
{
    public class Login
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
