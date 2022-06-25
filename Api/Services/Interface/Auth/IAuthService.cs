using Api.Database.Dtos.People;

namespace Api.Services.Interface.Auth
{
    public interface IAuthService
    {
        UserReadDto Register(UserWriteDto userWriteDto);

        UserReadDto Login(UserReadDto userReadDto);

        RefreshTokenDto RefreshToken(RefreshTokenDto refreshTokenDto);
    }
}
