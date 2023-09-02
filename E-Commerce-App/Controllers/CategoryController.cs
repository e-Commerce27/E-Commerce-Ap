using E_Commerce_App.Data;
using E_Commerce_App.Models.Interfaces;
using E_Commerce_App.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_App.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ECommerceContext _context;
        private readonly ICategory _category;
        public CategoryController(ICategory category)
        {
            _category = category;
        }

        public async Task<IActionResult> Index()
        {

            var allCategory = await _category.GetAllCategory();

            return View(allCategory);
        }
      
        public IActionResult Add()
        {
            Category cat = new Category();
            var add = _category.CreateCategory(cat);
            return View(cat);
        }
        [HttpPut]
        public async Task<IActionResult> Edit(int id , Category category)
        {
            
            if (!ModelState.IsValid) 
            {
                throw new  Exception(); 
            }

            var edit = await _category.UpdateCategory(id, category);
            return View(edit);
        }

        [HttpPost]
        public IActionResult Add(Category cat)
        {
            if (!ModelState.IsValid)
            {
                return View(cat);
            }

            return View(cat);

        }
        public IActionResult Edit(int Id)
        {
            Category cat = new Category()
            {
                Id = Id
               
            };
            return View(cat);
        }
        public IActionResult Delete()
        {


            return View();
        }
        [HttpPost]
        public IActionResult Edit(Category cat)
        {
            if (!ModelState.IsValid)
            {
                return View(cat);
            }

            return View(cat);

        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            
            return View();

        }
    }
}

