using Microsoft.AspNetCore.Identity;
using QLBaoDienTu.Models._Users;

namespace QLBaoDienTu.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        public UserService(UserManager<AppUser> userManager)
        {
            this._userManager = userManager;
        }

        public async Task<AppUser> GetUserById(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            return user;
        }
    }
}
