using E_Commerce_App.Models;
using E_Commerce_App.Models.DTO;
using E_Commerce_App.Models.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace E_Commerce_App.Controllers
{
    public class AuthController : Controller
    {
        private IUserService userService;
        private SignInManager<AuthUser> _signInManager;

        public AuthController(IUserService service, SignInManager<AuthUser> sim)
        {
            userService = service;
            _signInManager = sim;
        }

        /// <summary>
        /// Displays the index page.
        /// </summary>
        /// <returns>The index view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Displays the signup page (GET).
        /// </summary>
        /// <returns>The signup view.</returns>
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        /// <summary>
        /// Handles user registration (POST).
        /// </summary>
        /// <param name="data">The registration data.</param>
        /// <returns>Redirects to the home page on successful registration, or re-displays the signup page with errors.</returns>
        [HttpPost]
        public async Task<ActionResult<UserDTO>> Signup(RegisterDTO data)
        {
           var res = await userService.Register(data, this.ModelState);

            if (res != null)
            {
                return Redirect("/");
            }
            return null;
        }

        /// <summary>
        /// Handles user authentication (POST).
        /// </summary>
        /// <param name="data">The login credentials.</param>
        /// <returns>Redirects to the home page on successful authentication, or back to the index page with an error message.</returns>
        [HttpPost]
        public async Task<ActionResult<UserDTO>> Authenticate(Login data)
        {
            var user = await userService.Authenticate(data.Username, data.Password);
            if (user == null)
            {
                this.ModelState.AddModelError(String.Empty, "Invalid Login");
                return RedirectToAction("Index");
            }

            return Redirect("/");
        }

        /// <summary>
        /// Handles user logout.
        /// </summary>
        /// <returns>Redirects to the home page after logging the user out.</returns>
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
