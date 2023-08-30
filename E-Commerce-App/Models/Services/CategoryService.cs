using E_Commerce_App.Data;
using E_Commerce_App.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_App.Models.Services
{
    public class CategoryService : ICategory
    {
        private readonly ECommerceContext _context;
        public CategoryService(ECommerceContext context) 
        {
            _context = context;
        }
        public async Task<Category> CreateCategory(Category category)
        {
            _context.categories.Add(category);

            await _context.SaveChangesAsync();


            return category;
        }

        public async Task<Category> DeleteCategory(int Id)
        {
            Category category = await GetCategoryId(Id);
            _context.Entry(category).State = EntityState.Deleted;
            await _context.SaveChangesAsync();


            return category;
        }

        public async Task<List<Category>> GetAllCategory()
        {
            var category = await _context.categories.ToListAsync();

            return category;
        }

        public async Task<Category> GetCategoryId(int CategoryId)
        {
            Category? category = await _context.categories.FindAsync(CategoryId);

            return category;
        }

        public async Task<Category> UpdateCategory(int Id, Category category)
        {
            Category categories = await GetCategoryId(Id);
            categories.Name = category.Name;
            categories.Type= category.Type;
            categories.Amount= category.Amount;
            categories.products = categories.products;


            _context.Entry(categories).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            return categories;
        }
    }
}
