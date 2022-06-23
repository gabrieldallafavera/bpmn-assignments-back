using Api.Database.Entities.People;

namespace Api.Services.Interface.Auth
{
    public interface ITokenService
    {
        string CreateToken(User user);

        void SetRefreshToken(out string refreshToken, out DateTime tokenCreated, out DateTime tokenExpires);
    }
}
