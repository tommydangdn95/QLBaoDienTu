using QLBaoDienTu.Dtos;
using QLBaoDienTu.Models;
using QLBaoDienTu.Repositories;
using QLBaoDienTu.ViewModels._NewsViewModels;

namespace QLBaoDienTu.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;
        public NewsService(INewsRepository newsRepository)
        {
            this._newsRepository = newsRepository;
        }

        public async Task<Dtos.IResult> CreateNews(CreateNews createNews, Guid submitUserId)
        {
            var news = new News()
            {
                Title = createNews.Title,
                Content = createNews.Content,
                CategoryId = createNews.CategoryId,
                Slug = createNews.Slug,
                Status = (NewsStatus)createNews.Status,
                CreatedDate = DateTime.Now,
                CreatedBy = submitUserId,
                SubmittedByUserId = submitUserId
            };

            var result = await _newsRepository.CreateNews(news);
            if (result)
            {
                return Result.Failed("Could not create news");
            }

            return Result.Success("Create new news successfully");
        }
    }
}
