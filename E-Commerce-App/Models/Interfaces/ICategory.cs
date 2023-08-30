namespace E_Commerce_App.Models.Interfaces
{
    public interface ICategory
    {
        Task<Category> CreateCategory(Category category);

        Task<List<Category>> GetAllCategory();

        Task<Category> GetCategoryId(int CategoryId);

        Task<Category> UpdateCategory(int CategoryId, Category category);

        Task<Category> DeleteCategory(int CategoryId);
    }
}
