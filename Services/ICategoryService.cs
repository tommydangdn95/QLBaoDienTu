using QLBaoDienTu.Models;
using QLBaoDienTu.ViewModels._CategoryViewModels;
using QLBaoDienTu.Dtos;
using IResult = QLBaoDienTu.Dtos.IResult;

namespace QLBaoDienTu.Services
{
    public interface ICategoryService
    {
        public Task<IResult> CreateCategory(CreateCategory viewModel, Guid userId);
        public Task<IResult> UpdateCategory(UpdateCategory viewModel, Guid userId);
    }
}
