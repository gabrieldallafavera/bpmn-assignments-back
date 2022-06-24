using Api.Services.Interface.Auth;
using Api.Services.Services.Auth;

namespace Api.Scopes
{
    public static partial class Scope
    {
        static partial void ScopeServices(IServiceCollection services)
        {
            services.AddScoped<IPasswordHashService, PasswordHashService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IClaimService, ClaimService>();
            services.AddTransient<IAuthService, AuthService>();
        }
    }
}
