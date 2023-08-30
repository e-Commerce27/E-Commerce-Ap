namespace E_Commerce_App.Models.Interface
{
    public interface IProduct
    {
        Task<Product> CreateProduct(Product product);
        Task<Product> GetProduct(int id);
        Task <List<Product>> GetAllProducts();
        Task<Product> UpdateProduct(Product product,int productId);
        Task DeleteProduct(int id);
    }
}
