using E_Commerce_App.Models;
using E_Commerce_App.Models.Interface;
using E_Commerce_App.Models.Interfaces;
using E_Commerce_App.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E_Commerce_App.Pages.Account
{
    public class AllProductModel : PageModel
    {
        private readonly IProduct _product;
        [BindProperty]
        public List<Product> products { get; set; }
        public AllProductModel(IProduct pro)
        {
            _product = pro;            

        }
        public ProductAndCategoryVM categories { get; set; }   
        
        public async Task OnGet()
        {
            products = await _product.GetAllProducts();
           
        }

    }
}
