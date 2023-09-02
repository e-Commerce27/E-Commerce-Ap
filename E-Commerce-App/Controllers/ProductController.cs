using E_Commerce_App.Data;
using E_Commerce_App.Models;
using E_Commerce_App.Models.Interface;
using E_Commerce_App.Models.Interfaces;
using E_Commerce_App.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace E_Commerce_App.Controllers
{
    public class ProductController : Controller
    {
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

        public async Task<IActionResult>GetproductbyId(int id)
        {
            var categorybyId = await _product.GetProduct(id);

            ProductAndCategoryVM products = new ProductAndCategoryVM()
            {
                Category = categorybyId.category,
            };
            return View(products);
        }

        public IActionResult Add()
        {
            var product = new Product();
            return View(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>>Add(Product product)
         {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            await _product.CreateProduct(product);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int productId)
        {
            var product = await _product.GetProduct(productId);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int productId, Product product)
        {
            if (productId != product.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(product);
            }

            await _product.UpdateProduct(productId, product);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int productId)
        {
            var product = await _product.GetProduct(productId);
            if (product == null)
            {
                return NotFound();
            }

            await _product.DeleteProduct(productId);
            return RedirectToAction("Index");
        }
    }
}
