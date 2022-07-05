using Api.Services.Interface.Auth;
using System.Security.Claims;

namespace Api.Services.Services.Auth
{
    public class ClaimService : IClaimService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public object GetClaims()
        {
            object claims = new { };

            if (_httpContextAccessor.HttpContext != null)
            {
                claims = new
                {
                    nameIdentifier = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier),
                    name = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name),
                    email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email),
                    role = _httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(x => x.Value).ToList()
                };
            }

            return claims;
        }
    }
}
