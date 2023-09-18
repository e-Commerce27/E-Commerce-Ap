using E_Commerce_App.Models.DTO;
using E_Commerce_App.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace E_Commerce_App.Pages.Account
{
    public class SignUpModel : PageModel
    {
        private IUserService _userService;

        public SignUpModel(IUserService user)
        {
            _userService = user;
        }

        [BindProperty]
        public RegisterDTO register { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                register.Roles = new List<string>() { "User" };
                var result = await _userService.Register(register, this.ModelState);

                if (result != null)
                {
                    return RedirectToAction("Index", "Account");
                }
                else
                {
                    // Handle the case where user registration failed
                    ModelState.AddModelError("", "User registration failed.");
                    return Page(); // Redisplay the form with error messages
                }
            }

            // If ModelState is not valid, the form will be redisplayed with validation errors
            return Page();
        }
    }
}
   
