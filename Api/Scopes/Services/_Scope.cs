using Api.Services.Interface.Auth;
using Api.Services.Interface.Email;
using Api.Services.Services.Auth;
using Api.Services.Services.Email;

namespace Api.Scopes
{
    public static partial class Scope
    {
        static partial void ScopeServices(IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IClaimService, ClaimService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPasswordHashService, PasswordHashService>();
        }
    }
}
