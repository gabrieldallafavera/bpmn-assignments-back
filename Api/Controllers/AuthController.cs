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
        /// Ter tamanho mínimo 8 caracteres.
        /// Deve ter somente letras e numero e caractere especial.
        /// Deve ter no mínimo uma letra maiúscula e minúscula.
        /// Deve ter no mínimo um numero.
        /// Deve ter no mínimo caractere especial.
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
        [HttpPost("register")/*, Authorize(Roles = "Admin")*/]
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
        /// <response code="400">Usuário ou senha errado.</response>
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
        ///         "token": "nreinioio289rcn932b84cm483u09hy839x2h8"
        ///     }
        /// </remarks>
        /// <param name="refreshTokenDto">Objeto RefreshTokenDto com o Token</param>
        /// <response code="200">Objeto RefreshTokenDto com o novo token</response>
        [HttpPost("refresh-token")]
        public ActionResult<RefreshTokenDto> RefreshToken(RefreshTokenDto refreshTokenDto)
        {
            return Ok(_authService.RefreshToken(refreshTokenDto));
        }
    }
}
