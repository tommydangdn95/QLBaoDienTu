using QLBaoDienTu.Dtos;
using QLBaoDienTu.Models;
using QLBaoDienTu.Repositories;
using QLBaoDienTu.ViewModels._CategoryViewModels;
using IResult = QLBaoDienTu.Dtos.IResult;

namespace QLBaoDienTu.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IResult> CreateCategory(CreateCategory viewModel, Guid userId)
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
            if (result)
            {
                return Result.Failed("Could not create category");
            }

            return Result.Success("Create new category successfully");
        }

        public async Task<IResult> UpdateCategory(UpdateCategory viewModel, Guid userId)
        {
            var existingCategory = await _categoryRepository.GetById(viewModel.Id);
            if (existingCategory == null)
            {
                return Result.Failed("Could not update category because category not found");
            }


            existingCategory.Name = viewModel.Name;
            existingCategory.Description = viewModel.Description;
            existingCategory.IsActive = viewModel.IsActive;
            existingCategory.UpdatedDate = DateTime.Now;
            existingCategory.UpdatedBy = userId;

            var result = await _categoryRepository.UpdateCategory(existingCategory, userId);
            if (result)
            {
                return Result.Failed("Could not update category");
            }

            return Result.Success("Update category successfully");
        }
    }
}
