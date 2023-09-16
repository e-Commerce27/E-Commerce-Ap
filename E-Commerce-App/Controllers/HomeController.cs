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

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Remember(string name)
        {
            if (name != null)
            {
                // Set a cookie with the name in it...
                CookieOptions cookieOptions = new CookieOptions();
                
                cookieOptions.Expires = new DateTimeOffset(DateTime.Now.AddDays(7));
                HttpContext.Response.Cookies.Append("name", name, cookieOptions);
                return Content("Ok, Saved It");
            }

            return Content("Please provide a name");

        }

        [Authorize]
        public IActionResult Iam()
        {
            // app.get('/home/iam', (req,res) => {});
            string name = HttpContext.Request.Cookies["name"];
            ViewData["name"] = name;
            return View();
        }
    }
}
