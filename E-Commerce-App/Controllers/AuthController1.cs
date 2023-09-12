
using E_Commerce_App.Models;
using E_Commerce_App.Models.DTO;
using E_Commerce_App.Models.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_App.Controllers
{
    public class AuthController : Controller
    {

        // Use DI to bring in the user service
        private IUserService userService;
        private SignInManager<AuthUser> _signInManager;

        public AuthController(IUserService service, SignInManager<AuthUser> sim)
        {
            userService = service;
            _signInManager = sim;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> Signup(RegisterDTO data)
        {
            
           // data.Roles = new List<string>() { "Administrator" };

            // Create a user with the user service
             await userService.Register(data, this.ModelState);

            if (ModelState.IsValid)
            {
                return Redirect("/");
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> Authenticate(Login data)
        {
            // Check with the user service to ... see if we're in the system
            var user = await userService.Authenticate(data.Username, data.Password);
            if (user == null)
            {
                this.ModelState.AddModelError(String.Empty, "Invalid Login");
                return RedirectToAction("Index");
            }

            return Redirect("/");
            }
        
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}