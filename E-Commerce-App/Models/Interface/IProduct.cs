namespace E_Commerce_App.Models.Interface
{
    public interface IProduct
    {
        Task<Product> CreateProduct(Product product ,string imgUrl);
        Task<Product> GetProduct(int id);
        Task <List<Product>> GetAllProducts();
        Task<Product> UpdateProduct(int productId, Product product,string imgUrl);
        Task<Product> DeleteProduct(int id);
        Task<string> Upload(IFormFile file);
    }
}
