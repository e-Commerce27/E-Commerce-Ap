using Castle.Core.Configuration;
using E_Commerce_App.Data;
using E_Commerce_App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace TestProject1
{
   
    public  abstract class Mock : IDisposable
    {
        private readonly SqliteConnection _connection;
         protected readonly ECommerceContext _context;
       
        public Mock()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _context = new ECommerceContext(new DbContextOptionsBuilder<ECommerceContext>()
                .UseSqlite(_connection).Options);

            _context.Database.EnsureCreated();


        }

        protected async Task<Category> createAndsaveCategory()
        {
            var category = new Category {
                Name = "Fruits and vegetables", 
                Img = "https://c4.wallpaperflare.com/wallpaper/311/699/596/fruit-allsorts-pineapple-melon-wallpaper-preview.jpg", 
                Type = "category1" };

            _context.Categories.Add(category);

            await _context.SaveChangesAsync();

           // Assert.NotEqual(0, category.Id);
            return category;
        }

        protected async Task<Product> createAndSaveProduct()
        {
            var product = new Product { Name = "Tomato", Price = 2, Description = "item1", CategoryId = 1, Image = "" };
           

            _context.Prodects.Add(product);

            await _context.SaveChangesAsync();

            Assert.NotEqual(0, product.Id);
            return product;
        }

    

        public void Dispose()
        {
            _context?.Dispose();
            _connection?.Dispose();
            


        }
    }
}