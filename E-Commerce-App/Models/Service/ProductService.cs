//------------------------------------------------------//------------------------------------------------------//------------------------------------------------------//------------------------------------------


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

        // Constructor to initialize ECommerceContext and IConfiguration
        public ProductService(ECommerceContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Creates a new product and saves it to the database.
        /// </summary>
        /// <param name="product">The product to be created.</param>
        /// <param name="imgUrl">The URL of the product's image.</param>
        /// <returns>The created product, or null if creation fails.</returns>
        public async Task<Product> CreateProduct(Product product, string imgUrl)
        {
            Product newProduct = new Product()
            {
                Name = product.Name,
                Description = product.Description,
                ExpiryDate = DateTime.Now,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Image = imgUrl,


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

        /// <summary>
        /// Retrieves all products from the database.
        /// </summary>
        /// <returns>A list of all products.</returns>
        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Prodects.ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>The product with the specified ID.</returns>
        public async Task<Product> GetProduct(int id)
        {
            return await _context.Prodects.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Updates a product in the database.
        /// </summary>
        /// <param name="productId">The ID of the product to be updated.</param>
        /// <param name="product">The updated product data.</param>
        /// <param name="imgUrl">The URL of the updated product image.</param>
        /// <returns>The updated product, or null if update fails.</returns>
        public async Task<Product> UpdateProduct(int productId, Product product, string imgUrl)
        {
            var pro = await _context.Prodects.FindAsync(productId);
            if (pro != null)
            {


                pro.Name = product.Name;
                pro.Description = product.Description;
                pro.Price = product.Price;
                pro.CategoryId = product.CategoryId;
                if(imgUrl != null)
                {

                pro.Image = imgUrl;
                }
                _context.Entry(pro).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return pro;
        }

        /// <summary>
        /// Deletes a product from the database.
        /// </summary>
        /// <param name="id">The ID of the product to be deleted.</param>
        /// <param name="pro">The product to be deleted.</param>
        /// <returns>The deleted product, or null if deletion fails.</returns>
        public async Task<Product> DeleteProduct(int id, Product pro)
        {
            Product deleteProduct = await _context.Prodects.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (deleteProduct != null)
            {

                _context.Prodects.Remove(deleteProduct);
                await _context.SaveChangesAsync();
                return deleteProduct;
            }
            return null;
        }

        /// <summary>
        /// Uploads a file to Azure Blob Storage and returns the URL.
        /// </summary>
        /// <param name="file">The file to be uploaded.</param>
        /// <returns>The URL of the uploaded file.</returns>
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
