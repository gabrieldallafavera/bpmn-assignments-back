using Api.Services.Interface.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Models.People;

namespace Api.Controllers
{
    [ApiController]
    [Route("autentication")]
    [Produces("application/json")]
    [ApiExplorerSettings(GroupName = "Autenticação")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IClaimService _claimService;

        public AuthController(IAuthService authService, IClaimService claimService)
        {
            _authService = authService;
            _claimService = claimService;
        }

        /// <summary>
        /// Buscar Claims
        /// </summary>
        /// <response code="200">Objeto anonimo com as Claims</response>
        [HttpGet, Authorize]
        public ActionResult<object> GetClaims()
        {
            return Ok(_claimService.GetClaims());
        }

        /// <summary>
        /// Registra um novo usuário
        /// </summary>
        /// <remarks>
        /// Padrão das senhas:
        /// 
        ///     Ter tamanho mínimo 8 caracteres.
        ///     Deve ter somente letras e numero e caractere especial.
        ///     Deve ter no mínimo uma letra maiúscula e minúscula.
        ///     Deve ter no mínimo um numero.
        ///     Deve ter no mínimo caractere especial.
        /// 
        /// Exemplo de request:
        /// 
        ///     Post /UserRequest
        ///     {
        ///         "name": "Exemplo Nome",
        ///         "username": "exemploUserName",
        ///         "email": "exemplo@email.com",
        ///         "password": "MyPassword",
        ///         "confirmPassword": "MyPassword",
        ///         "userRoles": [
        ///             {
        ///                 "role" : "Admin"
        ///             },
        ///             {
        ///                 "role" : "User"
        ///             }
        ///         ]
        ///     }
        /// </remarks>
        /// <param name="userRequest">Objeto UserRequest</param>
        /// <response code="200">Retorno o objeto UserReadDto criado</response>
        [HttpPost("register"), Authorize(Roles = "Admin")]
        public ActionResult<UserResponse> Register(UserRequest userRequest)
        {
            return Ok(_authService.Register(userRequest));
        }

        /// <summary>
        /// Realiza login no sistema
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        /// 
        ///     Post /UserResponse
        ///     {
        ///         "username": "ExemploUserName", /* Remover se tiver email */
        ///         "email": "exemplo@email.com", /* Remover se tiver username */
        ///         "password": "MyPassword"
        ///     }
        /// </remarks>
        /// <param name="userResponse">Objeto UserResponse com usuário ou email e senha</param>
        /// <response code="200">Retorna o objeto UserReadDto com o token</response>
        [HttpPost("login")]
        public async Task<ActionResult<UserResponse>> Login(UserResponse userResponse)
        {
            return Ok(await _authService.Login(userResponse));
        }

        /// <summary>
        /// Renovar token
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        /// 
        ///     Post /RefreshTokenResponse
        ///     {
        ///         "token": "nreinioio289rcn932b84cm483u09hy839x2h8"
        ///     }
        /// </remarks>
        /// <param name="refreshTokenResponse">Objeto RefreshTokenResponse com o Token</param>
        /// <response code="200">Objeto RefreshTokenDto com o novo token</response>
        [HttpPost("refresh-token")]
        public ActionResult<RefreshTokenResponse> RefreshToken(RefreshTokenResponse refreshTokenResponse)
        {
            return Ok(_authService.RefreshToken(refreshTokenResponse));
        }

        /// <summary>
        /// Reenviar verificação de email
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        /// 
        ///     Post /UserResponse
        ///     {
        ///         "id": 2
        ///     }
        /// </remarks>
        /// <param name="userResponse">Objeto UserResponse com o Id</param>
        /// <response code="204">Email de confirmação enviado</response>
        [HttpPost("resend-verify-email")]
        public IActionResult ResendVerifyEmail(UserResponse userResponse)
        {
            _authService.ResendVerifyEmail(userResponse);

            return NoContent();
        }

        /// <summary>
        /// Verifica o email
        /// </summary>
        /// <param name="token">Token enviado por param</param>
        /// <response code="204">Email verificado</response>
        [HttpPost("verify-email/{token}")]
        public IActionResult VerifyEmail(string token)
        {
            _authService.VerifyEmail(token);

            return NoContent();
        }

        /// <summary>
        /// Solicitar recuperação de senha
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        /// 
        ///     Post /UserResponse
        ///     {
        ///         "username": "ExemploUserName", /* Remover se tiver email */
        ///         "email": "exemplo@email.com" /* Remover se tiver username */
        ///     }
        /// </remarks>
        /// <param name="userResponse">Objeto UserResponse com nome do usuário ou senha</param>
        /// <response code="204">Email enviado</response>
        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword(UserResponse userResponse)
        {
            _authService.ForgotPassword(userResponse);

            return NoContent();
        }

        /// <summary>
        /// Redefine a senha
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        /// 
        ///     Post /ResetPasswordRequest
        ///     {
        ///         "password": "ExemploDeSenha",
        ///         "confirmPassword": "ExemploDeSenha"
        ///     }
        /// </remarks>
        /// <param name="token">Token enviado por param</param>
        /// <param name="resetPasswordRequest">Objeto ResetPasswordRequest com senha e confirmação</param>
        /// <response code="204">Senha redefinida</response>
        [HttpPost("reset-password/{token}")]
        public IActionResult ResetPassword(string token, ResetPasswordRequest resetPasswordRequest)
        {
            _authService.ResetPassword(token, resetPasswordRequest);

            return NoContent();
        }
    }
}
