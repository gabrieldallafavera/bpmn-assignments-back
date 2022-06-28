using Api.Models.People;

namespace Api.Services.Interface.Auth
{
    public interface IAuthService
    {
        UserResponse Register(UserRequest userRequest);

        Task<UserResponse> Login(UserResponse userResponse);

        RefreshTokenResponse RefreshToken(RefreshTokenResponse refreshTokenResponse);

        void ResendVerifyEmail(UserResponse userResponse);

        void VerifyEmail(string token);

        void ForgotPassword(UserResponse userResponse);

        void ResetPassword(string token, ResetPasswordRequest resetPasswordRequest);
    }
}
