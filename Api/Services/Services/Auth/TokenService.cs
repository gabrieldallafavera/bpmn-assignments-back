using Api.Services.Interface.Auth;
using Api.Database.Entities.People;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Api.Services.Services.Auth
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email)
            };

            if (user.UserRoles != null)
            {
                foreach (var item in user.UserRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item.Role));
                }
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public void SetRefreshToken(out string token, out DateTime created, out DateTime expires)
        {
            CookieOptions cookieOptions = SetToken(out token, out created, out expires);
            
            _httpContextAccessor?.HttpContext?.Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        public void SetResetPassword(out string token, out DateTime created, out DateTime expires)
        {
            CookieOptions cookieOptions = SetToken(out token, out created, out expires);

            _httpContextAccessor?.HttpContext?.Response.Cookies.Append("resetPassword", token, cookieOptions);
        }

        public void SetVerifyEmail(out string token, out DateTime created, out DateTime expires)
        {
            CookieOptions cookieOptions = SetToken(out token, out created, out expires);

            _httpContextAccessor?.HttpContext?.Response.Cookies.Append("verifyEmail", token, cookieOptions);
        }

        private CookieOptions SetToken(out string token, out DateTime created, out DateTime expires)
        {
            token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            created = DateTime.Now;
            expires = DateTime.Now.AddDays(7);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires
            };

            return cookieOptions;
        }
    }
}
