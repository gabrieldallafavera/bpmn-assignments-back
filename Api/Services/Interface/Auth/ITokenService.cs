using Api.Database.Entities.People;

namespace Api.Services.Interface.Auth
{
    public interface ITokenService
    {
        string CreateToken(User user);

        void SetRefreshToken(out string token, out DateTime created, out DateTime expires);

        void SetResetPassword(out string token, out DateTime created, out DateTime expires);

        void SetVerifyEmail(out string token, out DateTime created, out DateTime expires);
    }
}
