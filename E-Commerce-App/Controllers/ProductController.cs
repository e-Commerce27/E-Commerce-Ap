//--------------------------------------------------------------------------------------------------------------------------------------//------------------------------------------------------------//

using E_Commerce_App.Models;
using E_Commerce_App.Models.Interface;
using E_Commerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using Document = E_Commerce_App.Models.Document;

namespace E_Commerce_App.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct _product;
        private readonly ICategory _category;
        private readonly HttpClient _httpClient;
        private const string ApiKey = "YourApiKeyHere"; // Insert your API key here
        private readonly IConfiguration _configuration;

        public ProductController(IProduct product, IConfiguration configuration)
        {
            _product = product;
            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        /// <summary>
        /// Displays a list of all products.
        /// </summary>
        /// <returns>The view with a list of products.</returns>
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var allProducts = await _product.GetAllProducts();
            return View(allProducts);
        }

        /// <summary>   
        /// Displays the view for adding a new product.
        /// </summary>
        /// <returns>The view for adding a product.</returns>
        [Authorize(Roles = "Administrator, Editor")]
        public async Task<IActionResult> Add()
        {
            var product = new Product();
            return View(product);
        }

        /// <summary>
        /// Handles the form submission for adding a new product.
        /// </summary>
        /// <param name="product">The new product data.</param>
        /// <param name="file">The uploaded image file.</param>
        /// <returns>Redirects to the product index on successful addition.</returns>
        [Authorize(Roles = "Administrator,Editor")]
        [HttpPost]
        public async Task<ActionResult<Product>> Add(Product product, IFormFile file)
        {
            ModelState.Remove("file");
            var imgeUrl = "";
            if (file != null)
                imgeUrl = await _product.Upload(file);
            else
                imgeUrl = "https://www.flaticon.com/free-icon/takeout-bag_11520614?related_id=11520614";

            if (!ModelState.IsValid)
            {
                return View(product);
            }
            await _product.CreateProduct(product, imgeUrl);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Displays the view for editing a product.
        /// </summary>
        /// <param name="productId">The ID of the product to edit.</param>
        /// <returns>The view for editing the product.</returns>
        [HttpGet]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> Edit(int productId)
        {
            var product = await _product.GetProduct(productId);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        /// <summary>
        /// Handles the form submission for editing a product.
        /// </summary>
        /// <param name="productId">The ID of the product being edited.</param>
        /// <param name="product">The updated product data.</param>
        /// <param name="file">The uploaded image file.</param>
        /// <returns>Redirects to the product index on successful edit.</returns>
        [Authorize(Roles = "Editor")]
        [HttpPost]
        public async Task<IActionResult> Edit(int productId, Product product, IFormFile file)
        {
            Product imageprodcut = await _product.GetProduct(productId);
            var imgUrl = imageprodcut.Image;

            if (file != null)
            {

                imgUrl = await _product.Upload(file);
            }

            if (productId != product.Id)
            {
                return NotFound();
            }

            ModelState.Remove("file");
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            ViewBag.IsEditor = User.IsInRole("Editor");
            await _product.UpdateProduct(productId, product, imgUrl);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Displays the view for viewing product details.
        /// </summary>
        /// <param name="productId">The ID of the product to view.</param>
        /// <returns>The view with details of the specified product.</returns>
        public async Task<IActionResult> Details(int productId)
        {
            Product pro = await _product.GetProduct(productId);


            return View(pro);
        }

        /// <summary>
        /// Handles the deletion of a product.
        /// </summary>
        /// <param name="productId">The ID of the product to delete.</param>
        /// <param name="pro">The product data.</param>
        /// <returns>Redirects to the product index on successful deletion.</returns>
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int productId, Product pro)
        {
            var product = await _product.GetProduct(productId);
            if (product == null)
            {
                return NotFound();
            }

            await _product.DeleteProduct(productId, pro);
            return RedirectToAction("Index");   
        }

        /// <summary>
        /// Generates an image from a Google search for the provided product name.
        /// </summary>
        /// <param name="productName">The name of the product to search for.</param>
        /// <returns>A file containing the image.</returns>
        public async Task<ActionResult> GenerateImageFromGoogle(string productName)
        {
            try
            {
                string apiKey = "AIzaSyDC6GrkAY6r3Q__KarSIFunOZDClensBiM";
                string searchUrl = $"https://www.googleapis.com/customsearch/v1";
                string cx = "97988a751eaeb42a2"; // Replace with your Custom Search Engine ID
                int numberOfResults = 1;

                // Construct the API request URL
                string apiUrl = $"{searchUrl}?q={productName}&cx={cx}&searchType=image&num={1}&key={apiKey}";

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResult = await response.Content.ReadAsStringAsync();
                        dynamic searchResult = JsonConvert.DeserializeObject(jsonResult);

                        if (searchResult.items != null && searchResult.items.Count > 0)
                        {
                            string imageUrl = searchResult.items[0].link;
                            HttpResponseMessage imageResponse = await client.GetAsync(imageUrl);
                            byte[] imageBytes = await imageResponse.Content.ReadAsByteArrayAsync();
                            return File(imageBytes, MediaTypeHeaderValue.Parse(imageResponse.Content.Headers.ContentType.ToString()).MediaType);
                        }
                    }
                }

                return Content("Image not found");
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }
        }

    }
}