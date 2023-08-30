using E_Commerce_App.Data;
using E_Commerce_App.Models;
using E_Commerce_App.Models.Services;
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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Category()
        {
            List<Category> category = new List<Category>()
            {
            new Category(){Name="shirt" , Type = "Clothes" , Amount="900pc" , products={ } },
            new Category(){Name="BMW" , Type = "Car" , Amount="50pc" , products={ } },
            new Category(){Name="Chocolate" , Type = "Food" , Amount="1000pc" , products={ } },
            new Category(){Name="Samsung" , Type = "Phone" , Amount="590pc" , products={ } },
            new Category(){Name="Light" , Type = "Tool" , Amount="2090pc" , products={ } }
            }
            ;


            return View(category);

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
    }
}