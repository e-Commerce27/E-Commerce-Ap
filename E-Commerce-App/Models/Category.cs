namespace E_Commerce_App.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Type { get; set; }

        public string? Amount { get; set; }

        public ICollection<Product>? products { get; set; }
    }
}
