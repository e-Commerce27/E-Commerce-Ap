using E_Commerce_App.Data;
using E_Commerce_App.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_App.Models.Service
{
    public class ProductService : IProduct
    {
        private readonly ECommerceContext _context;
        public ProductService(ECommerceContext context)
        {
            _context = context;
        }
        public Task<Product> CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Prodects.ToListAsync();
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _context.Prodects.FirstOrDefaultAsync(p => p.Id == id); // Filter by id
        }

        public Task<Product> UpdateProduct(Product product, int productId)
        {
            throw new NotImplementedException();
        }
    }
}