using Microsoft.EntityFrameworkCore;
using QLBaoDienTu.Models;

namespace QLBaoDienTu.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _dbContext;
        public CategoryRepository(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<bool> CreateCategory(Category category, Guid userId)
        {
            await _dbContext.Categories.AddAsync(category);
            var resutl = await _dbContext.SaveChangesAsync();
            return resutl > 0;
        }

        public async Task<bool> DeleteCategory(Guid categoryId, Guid userId)
        {
            var existingCategory = await GetById(categoryId);
            if (existingCategory == null)
            {
                throw new Exception($"Could not find category with id: ${categoryId}");
            }

            _dbContext.Categories.Remove(existingCategory);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<Category> GetById(Guid categoryId)
        {
            var category = await _dbContext.Categories.FindAsync(categoryId);
            return category;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            var categories = await _dbContext.Categories.Where(x => x.IsActive && !x.IsDeleted).ToListAsync();
            return categories;
        }

        public async Task<bool> UpdateCategory(Category category, Guid userId)
        {
            var existingCategory = await GetById(category.Id);
            if (existingCategory == null)
            {
                throw new Exception($"Could not find category with id: ${category.Id}");
            }


            _dbContext.Categories.Update(existingCategory);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}
