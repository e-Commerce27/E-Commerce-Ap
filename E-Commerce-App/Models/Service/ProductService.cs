using E_Commerce_App.Data;
using E_Commerce_App.Models.Interface;
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
        public async Task<Product> CreateProduct(Product product)
        {
            Product newProduct = new Product()
            {
                Name = product.Name,
                Description = product.Description,
                ExpiryDate = DateTime.Now,
                Price = product.Price,
                CategoryId = product.CategoryId,
            };
            _context.Entry(newProduct).State = EntityState.Added;
           
            await _context.SaveChangesAsync();
            if (newProduct.Id > 0)
            {
                return newProduct;
            }
            else
            {
                
                return null;
            }

        }

       
        public async Task<List<Product>> GetAllProducts()
        {
           return await _context.Prodects.ToListAsync();
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _context.Prodects.Where(x=>x.Id == id).FirstOrDefaultAsync(); 
        }

        public async Task<Product> UpdateProduct(int productId, Product product)
        {
            var pro = await _context.Prodects.FindAsync(productId);
            if (pro != null)
            {
                
          
                pro.Name = product.Name;
                pro.Description = product.Description;
                pro.Price = product.Price;
                pro.CategoryId = product.CategoryId;
                _context.Entry(pro).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return pro;
        
        }
        public async Task<Product> DeleteProduct(int id) {
            Product deleteProduct = await _context.Prodects.Where(x=>x.Id ==id).FirstOrDefaultAsync();
            if (deleteProduct != null)
            {
                _context.Prodects.Remove(deleteProduct);
                await _context.SaveChangesAsync();
                return deleteProduct;
            }
            return null;
        }

       
    }
}
