using Castle.Core.Configuration;
using E_Commerce_App.Models;
using E_Commerce_App.Models.Service;
using E_Commerce_App.Models.Services;
using E_Commerce_App.ViewModels;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace TestProject1
{
    public class UnitTest1 : Mock
    {
       
        [Fact]
        public async Task TestCraeteProduct()
        {
            var productService = new ProductService(_context, null);

            var productDto = await createAndSaveProduct();
           

            // Act
            await productService.CreateProduct(productDto, "ImageUrl");

            // Assert
            var createdProduct = await _context.Prodects.FirstOrDefaultAsync(p => p.Name == "Tomato");
            Assert.NotNull(createdProduct);
        }

        [Fact]
        public async Task TestCraeteCategory()
        {
            var categoryService = new CategoryService(_context, null);

            var category = await createAndsaveCategory();


            // Act
            await categoryService.CreateCategory(category, "ImageUrl");

            // Assert
            var careatecatrgory = await _context.Categories.FirstOrDefaultAsync(p => p.Type == "category1");
            Assert.NotNull(careatecatrgory);
        }

        [Fact]
        public async Task GetAllProducts()
        {
            // Arrange
            var productService = new ProductService (_context, null);

            // Act
            var products = await productService.GetAllProducts();

            // Assert
            Assert.NotNull(products);
            Assert.True(products.Count > 0);
        }

        [Fact]
        public async Task GetAllCategory()
        {
            // Arrange
            var categoryService = new CategoryService(_context, null);

            // Act
            var category = await categoryService.GetAllCategory();

            // Assert
            Assert.NotNull(category);
            Assert.True(category.Count > 0);
        }
        [Fact]
        public async Task GetProductById()
        {
            // Arrange
            var productService = new ProductService(_context, null);

            var product = await createAndSaveProduct();
           
            await productService.CreateProduct(product, "ImageUrl");

            var createdProduct = await _context.Prodects.FirstOrDefaultAsync(p => p.Name == "Tomato");

            // Act
            var retrievedProduct = await productService.GetProduct(createdProduct.Id);

            // Assert
            Assert.NotNull(retrievedProduct);
            Assert.Equal("Tomato", retrievedProduct.Name);
        }

        [Fact]
        public async Task GetCategoryById()
        {
            // Arrange
            var categoryService = new CategoryService(_context, null);

            var category = await createAndsaveCategory();
           ;
            await categoryService.CreateCategory(category, "ImageUrl");

            var createdcategory = await _context.Categories.FirstOrDefaultAsync(p => p.Name == "Fruits and vegetables");

            // Act
            var retrieved = await categoryService.GetCategoryId(createdcategory.Id);

            // Assert
            Assert.NotNull(retrieved);
            Assert.Equal("Fruits and vegetables", retrieved.Name);
        }
        public async Task UpdateProduct()
        {
            // Arrange
            var productService = new ProductService(_context, null);

            var product = await createAndSaveProduct();
           
            await productService.CreateProduct(product, "testImageUrl");

            var createdProduct = await _context.Prodects.FirstOrDefaultAsync(p => p.Name == "TestProduct");

            var updatedProductDto = new Product
            {
                Name = "Name",
                Description = "Description",
                Price = 10,
               
            };
            await productService.UpdateProduct( createdProduct.Id,updatedProductDto, "updatedImageUrl");

            // Act
            var updatedProduct = await _context.Prodects.FirstOrDefaultAsync(p => p.Id == createdProduct.Id);

            // Assert
            Assert.NotNull(updatedProduct);
            Assert.Equal("Name", updatedProduct.Name);
            Assert.Equal("Description", updatedProduct.Description);
            Assert.Equal(10, updatedProduct.Price);
           
        }
      
        public async Task UpdateCategory()
        {
            // Arrange
            var CategoryService = new CategoryService(_context, null);

            var cat = await createAndsaveCategory();

            await CategoryService.CreateCategory(cat, "testImageUrl");

            var created = await _context.Categories.FirstOrDefaultAsync(p => p.Name == "TestProduct");

            var updatedProductDto = new Category
            {
                Name = "Name",
                Type="category",
                Amount="80"

            };
            await CategoryService.UpdateCategory(created.Id, updatedProductDto, "updatedImageUrl");

            // Act
            var updated = await _context.Prodects.FirstOrDefaultAsync(p => p.Id == created.Id);

            // Assert
            Assert.NotNull(updated);
            Assert.Equal("Name", updated.Name);




        }
        [Fact]
        public async Task DeleteProduct()
        {
            // Arrange
            var productService = new ProductService(_context, null);

            var product = await createAndSaveProduct();           
            await productService.CreateProduct(product, "testImageUrl");

            var createdProduct = await _context.Prodects.FirstOrDefaultAsync(p => p.Name == "Tomato");

            // Act
            await productService.DeleteProduct(createdProduct.Id,product);

            // Assert
            var deletedProduct = await _context.Prodects.FirstOrDefaultAsync(p => p.Id == createdProduct.Id);
            Assert.Null(deletedProduct);
        }

        [Fact]
        public async Task DeleteCategry()
        {
            // Arrange
            var CategoryService = new CategoryService(_context, null);

            var cat = await createAndsaveCategory();
            await CategoryService.CreateCategory(cat, "testImageUrl");

            var created = await _context.Categories.FirstOrDefaultAsync(p => p.Name == "Fruits and vegetables");

            // Act
            await CategoryService.DeleteCategory(created.Id);

            // Assert
            var deletedProduct = await _context.Prodects.FirstOrDefaultAsync(p => p.Id == created.Id);
            Assert.Null(deletedProduct);
        }


    }
    }
