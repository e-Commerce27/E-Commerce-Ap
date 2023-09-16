using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using E_Commerce_App.Data;
using E_Commerce_App.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_App.Models.Services
{
    public class CategoryService : ICategory
    {
        private readonly ECommerceContext _context;
        private readonly IConfiguration _configuration;

        public CategoryService(ECommerceContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Creates a new category in the database.
        /// </summary>
        /// <param name="category">The category object to be created.</param>
        /// <param name="img">The URL of the image associated with the category.</param>
        /// <returns>The created category object.</returns>
        public async Task<Category> CreateCategory(Category category, String img)
        {
            category.Img = img;
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        /// <summary>
        /// Deletes a category with the specified ID from the database.
        /// </summary>
        /// <param name="Id">The ID of the category to be deleted.</param>
        /// <returns>The deleted category object.</returns>
        public async Task<Category> DeleteCategory(int Id)
        {
            Category cat = await _context.Categories
                .Where(x => x.Id == Id)
                .FirstOrDefaultAsync();

            if (cat != null)
            {
                _context.Categories.Remove(cat);
                await _context.SaveChangesAsync();
            }

            return cat;
        }

        /// <summary>
        /// Retrieves a list of all categories from the database.
        /// </summary>
        /// <returns>A list of categories.</returns>
        public async Task<List<Category>> GetAllCategory()
        {
            return await _context.Categories.ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific category by its ID, including associated products.
        /// </summary>
        /// <param name="CategoryId">The ID of the category to retrieve.</param>
        /// <returns>The category object with associated products.</returns>
        public async Task<Category> GetCategoryId(int CategoryId)
        {
            return await _context.Categories
                .Where(x => x.Id == CategoryId)
                .Include(x => x.products)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Updates the details of a category in the database.
        /// </summary>
        /// <param name="Id">The ID of the category to be updated.</param>
        /// <param name="category">The updated category object.</param>
        /// <param name="url">The URL of the image associated with the category.</param>
        /// <returns>The updated category object.</returns>
        public async Task<Category> UpdateCategory(int Id, Category category, string url)
        {
            var categories = await _context.Categories.FindAsync(Id);

            if (categories != null)
            {
                categories.Name = category.Name;
                categories.Type = category.Type;
                categories.Amount = category.Amount;
                if(url!= null)
                {
                 categories.Img = url;
                }

                _context.Entry(categories).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return categories;
        }

        /// <summary>
        /// Uploads an image to Azure Blob Storage and returns the URL.
        /// </summary>
        /// <param name="file">The image file to be uploaded.</param>
        /// <returns>The URL of the uploaded image.</returns>
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
