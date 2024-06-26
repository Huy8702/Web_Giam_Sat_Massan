using Microsoft.AspNetCore.Http;
using Web_Giam_Sat_Massan.Data;

namespace Web_Giam_Sat_Massan.Services
{
    public class RoleService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RoleService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsUserAdmin()
        {
            var role = _httpContextAccessor.HttpContext.Session.GetString("Role");
            return role != null && role.Trim() == "Admin"; // Trim() để loại bỏ khoảng trắng nếu có
        }
    }
}
