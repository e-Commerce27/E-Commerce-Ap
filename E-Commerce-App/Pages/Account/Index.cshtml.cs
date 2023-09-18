using E_Commerce_App.Models.DTO;
using E_Commerce_App.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E_Commerce_App.Pages.Account
{
    public class IndexModel : PageModel
    {
        private IUserService _userService;
        public IndexModel(IUserService user)
        {
            _userService = user; 
            
        }
        public void OnGet()
        {
        }
        [BindProperty]
        public Login login { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            var newUser = await _userService.Authenticate(login.Username, login.Password);
            if (newUser != null)
            {
                return RedirectToAction("Index","Product");
            }
            ModelState.AddModelError(nameof(login.Password), "Password or Username is not correct.");
            return Redirect("/");

        }
        
    }
}
