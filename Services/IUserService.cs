using QLBaoDienTu.Models._Users;

namespace QLBaoDienTu.Services
{
    public interface IUserService
    {
        public Task<AppUser> GetUserById(Guid userId);
    }
}
