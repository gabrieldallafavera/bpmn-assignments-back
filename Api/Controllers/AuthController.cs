using Api.Services.Interface.Auth;
using Api.Database.Dtos.People;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        ///     Post /UserWriteDto
        ///     {
        ///         "Name": "Exemplo Nome",
        ///         "Username": "exemploUserName",
        ///         "Email": "exemplo@email.com",
        ///         "Password": "MyPassword",
        ///         "ConfirmPassword": "MyPassword",
        ///         "Role": "User"
        ///     }
        /// </remarks>
        /// <param name="userWriteDto">Objeto UserWriteDto</param>
        /// <response code="200">Retorno o objeto UserReadDto criado</response>
        [HttpPost("register"), Authorize(Roles = "Admin")]
        public ActionResult<UserReadDto> Register(UserWriteDto userWriteDto)
        {
            return Ok(_authService.Register(userWriteDto));
        }

        /// <summary>
        /// Realiza login no sistema
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        /// 
        ///     Post /UserDto
        ///     {
        ///         "Username": "ExemploUserName", /* Remover se tiver email */
        ///         "Email": "exemplo@email.com", /* Remover se tiver username */
        ///         "Password": "MyPassword"
        ///     }
        /// </remarks>
        /// <param name="userReadDto">Objeto UserReadDto com usuário ou email e senha</param>
        /// <response code="200">Retorna o objeto UserReadDto com o token</response>
        [HttpPost("login")]
        public ActionResult<UserReadDto> Login(UserReadDto userReadDto)
        {
            return Ok(_authService.Login(userReadDto));
        }

        /// <summary>
        /// Renovar token
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        /// 
        ///     Post /RefreshTokenDto
        ///     {
        ///         "Token": "nreinioio289rcn932b84cm483u09hy839x2h8"
        ///     }
        /// </remarks>
        /// <param name="refreshTokenDto">Objeto RefreshTokenDto com o Token</param>
        /// <response code="200">Objeto RefreshTokenDto com o novo token</response>
        [HttpPost("refresh-token")]
        public ActionResult<RefreshTokenDto> RefreshToken(RefreshTokenDto refreshTokenDto)
        {
            return Ok(_authService.RefreshToken(refreshTokenDto));
        }

        /// <summary>
        /// Reenviar verificação de email
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        /// 
        ///     Post /UserReadDto
        ///     {
        ///         "Id": 2
        ///     }
        /// </remarks>
        /// <param name="userReadDto">Objeto UserReadDto com o Id</param>
        /// <response code="204">Email de confirmação enviado</response>
        [HttpPost("resend-verify-email")]
        public IActionResult ResendVerifyEmail(UserReadDto userReadDto)
        {
            _authService.ResendVerifyEmail(userReadDto);

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
        ///     Post /UserReadDto
        ///     {
        ///         "Username": "ExemploUserName", /* Remover se tiver email */
        ///         "Email": "exemplo@email.com" /* Remover se tiver username */
        ///     }
        /// </remarks>
        /// <param name="userReadDto">Objeto UserReadDto com nome do usuário ou senha</param>
        /// <response code="204">Email enviado</response>
        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword(UserReadDto userReadDto)
        {
            _authService.ForgotPassword(userReadDto);

            return NoContent();
        }

        /// <summary>
        /// Redefine a senha
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        /// 
        ///     Post /ResetPasswordDto
        ///     {
        ///         "Password": "ExemploDeSenha",
        ///         "ConfirmPassword": "ExemploDeSenha"
        ///     }
        /// </remarks>
        /// <param name="token">Token enviado por param</param>
        /// <param name="resetPasswordDto">Objeto ResetPasswordDto com senha e confirmação</param>
        /// <response code="204">Senha redefinida</response>
        [HttpPost("reset-password/{token}")]
        public IActionResult ResetPassword(string token, ResetPasswordDto resetPasswordDto)
        {
            _authService.ResetPassword(token, resetPasswordDto);

            return NoContent();
        }
    }
}
