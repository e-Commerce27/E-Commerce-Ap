using E_Commerce_App.Data;
using E_Commerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_App.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ECommerceContext _context;
        private readonly ICategory _category;
        public CategoryController (ICategory category)
        {
            _category = category;
            
        }
        public async Task<IActionResult> Index()
        {
            var allCategory = await _category.GetAllCategory();
            return View(allCategory);
        }
    }
}
