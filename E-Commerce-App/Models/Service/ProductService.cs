using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using E_Commerce_App.Data;
using E_Commerce_App.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_App.Models.Service
{
    public class ProductService : IProduct
    {
        private readonly ECommerceContext _context;
        private readonly IConfiguration _configuration;
        public ProductService(ECommerceContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<Product> CreateProduct(Product product,string imgUrl)
        {
            Product newProduct = new Product()
            {
                Name = product.Name,
                Description = product.Description,
                ExpiryDate = DateTime.Now,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Image = imgUrl
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

        public async Task<Product> UpdateProduct(int productId, Product product,string imgUrl)
        {
            var pro = await _context.Prodects.FindAsync(productId);
            if (pro != null)
            {
                
          
                pro.Name = product.Name;
                pro.Description = product.Description;
                pro.Price = product.Price;
                pro.CategoryId = product.CategoryId;
                pro.Image = imgUrl;
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


        public async Task<string> Upload(IFormFile file)
        {
            BlobContainerClient blob = new BlobContainerClient(_configuration.GetConnectionString("AzureStorage"), "images");
            await blob.CreateIfNotExistsAsync();
            // Check if the file exists
            if (file !=null)
            {
                BlobClient blobClient = blob.GetBlobClient(file.FileName);
                using var fileStream = file.OpenReadStream();
                BlobUploadOptions blobUploadOptions = new BlobUploadOptions()
                {
                    HttpHeaders = new BlobHttpHeaders { ContentType = file.ContentType }
                };
                if (!blobClient.Exists())
                {
                    await blobClient.UploadAsync(fileStream, blobUploadOptions);
                }
                return blobClient.Uri.ToString();
            }
            else
            {
                return null;
            }
        }

    }


}
