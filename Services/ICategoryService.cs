using QLBaoDienTu.Models;
using QLBaoDienTu.ViewModels._CategoryViewModels;

namespace QLBaoDienTu.Services
{
    public interface ICategoryService
    {
        public Task<bool> CreateCategory(CreateCategory viewModel, Guid userId);
    }
}
