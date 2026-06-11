using QLBaoDienTu.Models;

namespace QLBaoDienTu.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly AppDbContext _dbContext;
        public NewsRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateNews(News news)
        {
            await _dbContext.News.AddAsync(news);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateNews(News news)
        {
            _dbContext.News.Update(news);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}
