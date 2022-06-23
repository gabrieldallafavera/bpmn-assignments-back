using Api.Database.Dtos.People;

namespace Api.Services.Interface.Auth
{
    public interface IAuthService
    {
        Task<UserDto> Register(UserDto userDto);

        Task<UserDto> Login(UserDto userDto);

        Task<UserDto> RefreshToken(UserDto userDto);
    }
}
