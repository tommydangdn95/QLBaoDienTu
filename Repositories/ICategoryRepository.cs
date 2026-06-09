using QLBaoDienTu.Models;

namespace QLBaoDienTu.Repositories
{
    public interface ICategoryRepository
    {
        public Task<Category> GetById(Guid categoryId);
        public Task<List<Category>> GetCategoriesAsync();
        public Task<bool> CreateCategory(Category category, Guid userId);
        public Task<bool> UpdateCategory(Category category, Guid userId);
        public Task<bool> DeleteCategory(Guid categoryId, Guid userId);
    }
}
