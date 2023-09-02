using E_Commerce_App.Models;
using E_Commerce_App.Models.Interface;
using E_Commerce_App.Models.Interfaces;
using E_Commerce_App.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_App.Controllers
{
    public class ViewModelsController : Controller
    {
        private readonly ICategory _category;
        private readonly IProduct _product;
        public ViewModelsController(ICategory category)
        {
            _category = category;
                    
        }
        public async Task<IActionResult> Index(int id)
        {
            var allCategories = await _category.GetCategoryId(id);
            ProductAndCategoryVM prodectAndCategory = new ProductAndCategoryVM()
            {
               Category =allCategories,
               Product = allCategories.products.ToList()
            };
            return View(prodectAndCategory);
        }
      
    }
}
