using Api.Services.Interface.Auth;
using AutoMapper;
using Api.Database.Dtos.People;
using Api.Database.Entities.People;
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
        /// <param name="userDto">Objeto UserDto</param>
        /// <response code="200">Retorno o objeto UserDto criado</response>
        [HttpPost("register"), Authorize(Roles = "Admin")]
        public ActionResult<UserDto> Register(UserDto userDto)
        {
            try
            {
                return Ok(_authService.Login(userDto));
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Realiza login no sistema
        /// </summary>
        /// <param name="userDto">Objeto UserDto com usuário ou email e senha</param>
        /// <response code="200">Retorna o objeto UserDto com o token</response>
        /// <response code="400">Usuário ou senha errado.</response>
        [HttpPost("login")]
        public ActionResult<UserDto> Login(UserDto userDto)
        {
            try
            {
                return Ok(_authService.Login(userDto));
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Renovar token
        /// </summary>
        /// <response code="200">Objeto UserDto com o novo token</response>
        [HttpPost("refresh-token")]
        public ActionResult<UserDto> RefreshToken(UserDto userDto)
        {
            try
            {
                return Ok(_authService.RefreshToken(userDto));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
