using Microsoft.AspNetCore.Identity;
using System.Xml.Linq;

namespace QLBaoDienTu.Models._Users
{
    public class AppRole : IdentityRole<Guid>
    {
        /// <summary>
        /// Mô tả chi tiết về vai trò
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Ngày tạo vai trò
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Navigation: Quan hệ Role-User (Many-to-Many)
        /// </summary>
        public virtual ICollection<AppUserRole> UserRoles { get; set; } = new List<AppUserRole>();
    }
}
