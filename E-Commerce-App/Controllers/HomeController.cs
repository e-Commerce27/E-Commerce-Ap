using E_Commerce_App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace E_Commerce_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Displays the index view of the application.
        /// </summary>
        /// <returns>The index view.</returns>
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Displays the privacy policy view.
        /// </summary>
        /// <returns>The privacy policy view.</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Handles errors and displays the error view.
        /// </summary>
        /// <returns>The error view with error details.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Saves a provided name in a cookie.
        /// </summary>
        /// <param name="name">The name to save in the cookie.</param>
        /// <returns>A message indicating success or a request for a name.</returns>
        public IActionResult Remember(string name)
        {
            if (name != null)
            {
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = new DateTimeOffset(DateTime.Now.AddDays(7));
                HttpContext.Response.Cookies.Append("name", name, cookieOptions);
                return Content("Ok, Saved It");
            }

            return Content("Please provide a name");
        }

        /// <summary>
        /// Displays the "I am" view, showing the saved name from the cookie.
        /// </summary>
        /// <returns>The "I am" view with the saved name.</returns>
        [Authorize]
        public IActionResult Iam()
        {
            string name = HttpContext.Request.Cookies["name"];
            ViewData["name"] = name;
            return View();
        }
    }
}
