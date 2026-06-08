using Microsoft.AspNetCore.Identity;

namespace QLBaoDienTu.Utils
{
    public static class StringExtension
    {
        public static string ToError(this IEnumerable<IdentityError> errors)
        {
            return string.Join(".", errors.Select(e => e.Description));
        }

        public static string ToError(this IList<string> errors)
        {
            return string.Join(".", errors);
        }
    }
}
