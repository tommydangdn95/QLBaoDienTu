using QLBaoDienTu.Models;
using QLBaoDienTu.Repositories;
using QLBaoDienTu.ViewModels._CategoryViewModels;

namespace QLBaoDienTu.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> CreateCategory(CreateCategory viewModel, Guid userId)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = viewModel.Name,
                Description = viewModel.Description,
                IsActive = true,
                IsDeleted = false,
                CreatedDate = DateTime.Now,
                CreatedBy = userId
            };

            var result = await _categoryRepository.CreateCategory(category, userId);
            return result;
        }
    }
}
