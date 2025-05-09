using System.Net;
using System.Security.Claims;
using Student_Result_Management_System.Interfaces;

namespace Student_Result_Management_System.Services
{
    public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

        public int UserId
        {
            get
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("userId");
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    return userId;
                }
                return 0;
            }
        }

        public string UserName => _httpContextAccessor.HttpContext?.User?.FindFirst("fullname")?.Value ?? string.Empty;

        public string UserRole => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;
        public IPAddress UserIpAddress => _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress ?? IPAddress.None;
    }
}