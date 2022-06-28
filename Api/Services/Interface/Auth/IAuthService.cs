using Api.Models.People;

namespace Api.Services.Interface.Auth
{
    public interface IAuthService
    {
        Task<UserResponse> Register(UserRequest userRequest);

        Task<UserResponse> Login(UserResponse userResponse);

        Task<RefreshTokenResponse> RefreshToken(RefreshTokenResponse refreshTokenResponse);

        Task ResendVerifyEmail(UserResponse userResponse);

        Task VerifyEmail(string token);

        Task ForgotPassword(UserResponse userResponse);

        Task ResetPassword(string token, ResetPasswordRequest resetPasswordRequest);
    }
}
