using QLBaoDienTu.Dtos;
using QLBaoDienTu.ViewModels._NewsViewModels;
using IResult = QLBaoDienTu.Dtos.IResult;

namespace QLBaoDienTu.Services
{
    public interface INewsService
    {
        Task<IResult> CreateNews(CreateNews createNews, Guid submitUserId);
    }
}
