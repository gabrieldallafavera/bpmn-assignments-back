using Api.Database.Dtos.People;

namespace Api.Services.Interface.Auth
{
    public interface IAuthService
    {
        UserReadDto Register(UserWriteDto userWriteDto);

        UserReadDto Login(UserReadDto userReadDto);

        RefreshTokenDto RefreshToken(RefreshTokenDto refreshTokenDto);

        void ResendVerifyEmail(UserReadDto userReadDto);

        void VerifyEmail(string token);

        void ForgotPassword(UserReadDto userReadDto);

        void ResetPassword(string token, ResetPasswordDto resetPasswordDto);
    }
}
