using E_Commerce_App.Data;
using E_Commerce_App.Models;
using E_Commerce_App.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_App.Controllers
{
    public class ProductController : Controller
    {
        private readonly ECommerceContext _context;
        private readonly IProduct _product;
        public ProductController(IProduct product)
        {
           _product = product;
        }
     
        public async Task<IActionResult> Index()
        {
           
            var allProducts = await _product.GetAllProducts();
                       
            return View(allProducts);
        }
    }
}
