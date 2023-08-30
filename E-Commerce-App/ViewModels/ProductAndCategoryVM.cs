using E_Commerce_App.Models;

namespace E_Commerce_App.ViewModels
{
    public class ProductAndCategoryVM
    {
        public IEnumerable<Product> Product { get; set; }
        public Category Category { get; set; }
    }
}
