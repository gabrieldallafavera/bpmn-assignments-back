using Api.Services.Interface.Auth;
using Api.Services.Services.Auth;

namespace Api.Scopes
{
    internal static partial class Scope
    {
        static partial void ScopeAuth(IServiceCollection services)
        {
            services.AddScoped<IPasswordHashService, PasswordHashService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IClaimService, ClaimService>();
        }
    }
}
