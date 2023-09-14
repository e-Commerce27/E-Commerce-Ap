using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using E_Commerce_App.Data;
using E_Commerce_App.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace E_Commerce_App.Models.Services
{
    public class CategoryService : ICategory

    {
        private readonly ECommerceContext _context;
        private readonly IConfiguration _configuration;
        public CategoryService(ECommerceContext context , IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<Category> CreateCategory(Category category , String img)
        {
            _context.Categories.Add(category);

            await _context.SaveChangesAsync();


            return category;
        }

        public async Task<Category> DeleteCategory(int Id)
        {
            Category cat = await _context.Categories.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (cat != null)
            {
                _context.Categories.Remove(cat);
                await _context.SaveChangesAsync();
                return cat;
            }
            return null;

           
        }

        public async Task<List<Category>> GetAllCategory()
        {
            var category = await _context.Categories.ToListAsync();

            return category;
        }

        public async Task<Category> GetCategoryId(int CategoryId)
        {
            Category category = await _context.Categories.Where(x=>x.Id==CategoryId).Include(x=>x.products).FirstOrDefaultAsync();

            return category;
        }

        public async Task<Category> UpdateCategory(int Id, Category category, string url)
        {
            var categories = await _context.Categories.FindAsync(Id);
            if(categories != null)
            {
                categories.Name = category.Name;
                categories.Type = category.Type;
                categories.Amount = category.Amount;
                categories.Img = category.Img;
                _context.Entry(categories).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
          

            return categories;
        }

        public async Task<string> Upload(IFormFile file)
        {
            BlobContainerClient blob = new BlobContainerClient(_configuration.GetConnectionString("AzureStorage"), "images");
            await blob.CreateIfNotExistsAsync();
            // Check if the file exists
            if (file != null)
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