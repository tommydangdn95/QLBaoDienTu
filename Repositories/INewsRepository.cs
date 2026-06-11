using QLBaoDienTu.Models;

namespace QLBaoDienTu.Repositories
{
    public interface INewsRepository
    {
        public Task<bool> CreateNews(News news);
        public Task<bool> UpdateNews(News news);
    }
}
