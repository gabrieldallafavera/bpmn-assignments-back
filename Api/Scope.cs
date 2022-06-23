using Services.Interface.People;
using Services.Services.People;

namespace Api
{
    public class Scope
    {
        public static void ScopePeople(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
