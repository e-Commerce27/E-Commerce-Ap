
using E_Commerce_App.Data;
using E_Commerce_App.Models;
using E_Commerce_App.Models.Interface;
using E_Commerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Displays a list of all categories.
        /// </summary>
        /// <returns>The view with a list of categories.</returns>
        /// 
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var allCategory = await _category.GetAllCategory();
            return View(allCategory);
        }

        /// <summary>
        /// Retrieves a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>The view with details of the specified category.</returns>
        [HttpGet]
        public async Task<IActionResult> GetCategorytbyId(int id)
        {
            var categorybyId = await _category.GetCategoryId(id);
            return View(categorybyId);
        }

        /// <summary>
        /// Displays the view for editing a category.
        /// </summary>
        /// <param name="IdCategory">The ID of the category to edit.</param>
        /// <returns>The view for editing the category.</returns>
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

        /// <summary>
        /// Handles the form submission for editing a category.
        /// </summary>
        /// <param name="IdCategory">The ID of the category being edited.</param>
        /// <param name="category">The updated category data.</param>
        /// <param name="file">The uploaded image file.</param>
        /// <returns>Redirects to the category index on successful edit.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int IdCategory, Category category, IFormFile file)
        {
            var imgCategory = await _category.Upload(file);
            ModelState.Remove("file");
            if (!ModelState.IsValid)
            {
                return View(category);
            }


            await _category.UpdateCategory(IdCategory, category, imgCategory);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Displays the view for adding a new category.
        /// </summary>
        /// <returns>The view for adding a category.</returns>
        public async Task<IActionResult> Add()
        {
            var cat = new Category();
            return View(cat);
        }

        /// <summary>
        /// Handles the form submission for adding a new category.
        /// </summary>
        /// <param name="cat">The new category data.</param>
        /// <param name="file">The uploaded image file.</param>
        /// <returns>Redirects to the category index on successful addition.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Category>> Add(Category cat, IFormFile file)
        {
            ModelState.Remove("file");
            var img = "";
            if (file != null)
            {
                img = await _category.Upload(file);
            }

            if (!ModelState.IsValid)
            {
                return View(cat);
            }
            await _category.CreateCategory(cat, img);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Displays the view for confirming category deletion.
        /// </summary>
        /// <param name="IdCategory">The ID of the category to delete.</param>
        /// <returns>The view for confirming category deletion.</returns>
        public async Task<IActionResult> Delete(int IdCategory)
        {
            var category = await _category.GetCategoryId(IdCategory);
            return View(category);
        }

        /// <summary>
        /// Handles the form submission for deleting a category.
        /// </summary>
        /// <param name="category">The category to delete.</param>
        /// <returns>Redirects to the category index on successful deletion.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Category category)
        {
            var category1 = await _category.GetCategoryId(category.Id);
            if (category1 != null)
            {
                await _category.DeleteCategory(category1.Id);
            }


            return RedirectToAction(nameof(Index));

        }
    }
}
