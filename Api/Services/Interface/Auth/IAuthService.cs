using Api.Database.Dtos.People;

namespace Api.Services.Interface.Auth
{
    public interface IAuthService
    {
        Task<UserReadDto> Register(UserWriteDto userWriteDto);

        Task<UserReadDto> Login(UserReadDto userReadDto);

        Task<RefreshTokenDto> RefreshToken(RefreshTokenDto refreshTokenDto);
    }
}
