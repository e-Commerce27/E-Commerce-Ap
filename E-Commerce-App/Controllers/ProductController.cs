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
//using E_Commerce_App.GoogleImageSearch; 
using Document = E_Commerce_App.Models.Document;

namespace E_Commerce_App.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct _product;
        private readonly ICategory _category;
        private readonly HttpClient _httpClient;
        private const string ApiKey = "AIzaSyDC6GrkAY6r3Q__KarSIFunOZDClensBiM";
        private readonly IConfiguration _configuration;
        public ProductController(IProduct product, IConfiguration configuration)
        {
            _product = product;
            _httpClient = new HttpClient();
           // _httpClient.DefaultRequestHeaders.Add("Ocp-Api-Subscription-Key", BingApiKey);
            _configuration = configuration;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var allProducts = await _product.GetAllProducts();
            return View(allProducts);
        }

        [Authorize(Roles = "Administrator, Editor")]
        public async Task<IActionResult> Add()
        {
           
            var product = new Product();
            return View(product);
        }
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

        [Authorize(Roles = "Editor")]
        [HttpPost]
        public async Task<IActionResult> Edit(int productId, Product product,IFormFile file)
        {
            Product imageprodcut = await _product.GetProduct(productId);
           var imgUrl = imageprodcut.Image;
            if (file != null)
            {
                
               imgUrl= await _product.Upload(file);
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
            await _product.UpdateProduct(productId, product,imgUrl);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int productId)
        {
            Product pro = await _product.GetProduct(productId);


            return View(pro);
        }

        [Authorize(Roles = "Administrator")]
        // [HttpPost]
        public async Task<IActionResult> Delete(int productId)
        {
            var product = await _product.GetProduct(productId);
            if (product == null)
            {
                return NotFound();
            }

            await _product.DeleteProduct(productId);
            return RedirectToAction("Index");
        }

        //public async Task<ActionResult> GenerateImageFromInternet(string productName)
        //{
        //    try
        //    {
        //        // Define the API URL
        //        string apiUrl = $"https://api.cognitive.microsoft.com/bing/v7.0/images/search?q={productName}&count=1";

        //        // Add API key to request headers
        //        _httpClient.DefaultRequestHeaders.Add("Ocp-Api-Subscription-Key", BingApiKey);

        //        // Make the API request
        //        HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            string jsonResult = await response.Content.ReadAsStringAsync();
        //            var searchResult = JsonConvert.DeserializeObject<BingImageSearchResult>(jsonResult);

        //            if (searchResult.value.Count > 0)
        //            {
        //                string imageUrl = searchResult.value[0].contentUrl;
        //                HttpResponseMessage imageResponse = await _httpClient.GetAsync(imageUrl);
        //                byte[] imageBytes = await imageResponse.Content.ReadAsByteArrayAsync();
        //                return File(imageBytes, MediaTypeHeaderValue.Parse(imageResponse.Content.Headers.ContentType.ToString()).MediaType);
        //            }
        //        }

        //        return Content("Image not found");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Content("Error: " + ex.Message);
        //    }
        //}
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
