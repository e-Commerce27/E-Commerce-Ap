using E_Commerce_App.Data;
using E_Commerce_App.Models;
using E_Commerce_App.Models.Interface;
using E_Commerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_App.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ECommerceContext _context;
        private readonly ICategory _category;
        private readonly IConfiguration _configuration;
        public CategoryController(ICategory category, IConfiguration configuration)
        {
            _category = category;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            var allCategory = await _category.GetAllCategory();
            return View(allCategory);
        }
        [HttpGet]
        public async Task<IActionResult> GetCategorytbyId(int id)
        {
            var categorybyId = await _category.GetCategoryId(id);


            return View(categorybyId);
        }

      
      
        public async Task<IActionResult> Edit(int IdCategory)
        {
            var category = await _category.GetCategoryId(IdCategory);
            var new_category = new Category()
            {
                Id = category.Id,
                Name = category.Name,
                Type = category.Type,
                Amount = category.Amount,
                Img = category.Img
            };
            return View(new_category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int IdCategory, Category category , IFormFile file)
        {
            var imgCategory = await _category.Upload(file);
            ModelState.Remove("file");
            if (!ModelState.IsValid)
            {
                return View(category);
            }

           
            await _category.UpdateCategory(IdCategory, category , imgCategory);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Add()
        {
            var cat = new Category();
            
           
            return View(cat);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Category>> Add(Category cat , IFormFile file)
        {
            ModelState.Remove("file");
            var img = "";
            if(file != null)
            {
                img = await _category.Upload(file);
            }
            
            if (!ModelState.IsValid)
            {
                return View(cat);
            }
await _category.CreateCategory(cat , img);
            return RedirectToAction("Index");   
        }

        /*public async Task<IActionResult> Delete(int IdCategory)
        {
            if (IdCategory == null )
            {
                return NotFound();
            }

            var category = await _category.GetCategoryId(IdCategory);
            if (category == null)
            {
                return NotFound();
            }

            return View("Index");
        }

        
        [HttpDelete]
        
        public async Task<IActionResult> DeleteCotegory(int IdCategory)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'ECommerceContext.Category'  is null.");
            }
            var category = await _context.Categories.FindAsync(IdCategory);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }*/
        public async Task<IActionResult> Delete(int IdCategory)
        {
            var category = await _category.GetCategoryId(IdCategory);
           
            return View(category);
        }

        [HttpPost , ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete( Category category)
        {

            var category1 = await _category.GetCategoryId(category.Id);
            if (category1 != null)
            {
             await  _category.DeleteCategory(category1.Id);
            }

            
            return RedirectToAction(nameof(Index));

        }


    }
}


