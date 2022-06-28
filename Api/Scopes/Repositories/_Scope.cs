using Api.Repositories.Interface.People;
using Api.Repositories.Repositories.People;

namespace Api.Scopes
{
    public static partial class Scope
    {
        static partial void ScopeRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenFunctionRepository, TokenFunctionRepository>();
        }
    }
}
