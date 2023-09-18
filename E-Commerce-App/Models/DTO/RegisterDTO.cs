using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_App.Models.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public List<string> Roles { get; set; }

        // Custom validation method for Roles
        public ValidationResult ValidateRoles()
        {
            if (Roles == null || Roles.Count == 0)
            {
                return new ValidationResult("At least one role is required.");
            }

            // Additional custom validation logic for roles can be added here

            return ValidationResult.Success;
        }
    }
}
