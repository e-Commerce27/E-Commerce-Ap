namespace E_Commerce_App.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public DateTime ExpiryDate { get; set; }
        public virtual Category? category { get; set; }
    }
}
