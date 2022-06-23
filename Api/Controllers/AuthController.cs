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
        public static User user = new User(); // Verificar para subistituir pela consulta do banco
        private readonly IMapper _mapper;
        
        private readonly IPasswordHashService _passwordHashService;
        private readonly ITokenService _tokenService;
        private readonly IClaimService _claimService;

        //Converter de IUserService para services necessário para reduzir a controller
        public AuthController(IMapper mapper, IPasswordHashService passwordHashService, ITokenService tokenService, IClaimService claimService)
        {
            _mapper = mapper;
            
            _passwordHashService = passwordHashService;
            _tokenService = tokenService;
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
            _passwordHashService.CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user = _mapper.Map<User>(userDto);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            // Criar método para salvar no banco

            return Ok(_mapper.Map<UserDto>(user));
        }

        //Verificar a necessidade de criar um refreshToken diretamente dentro da User

        /// <summary>
        /// Realiza login no sistema
        /// </summary>
        /// <param name="userDto">Objeto UserDto com usuário ou email e senha</param>
        /// <response code="200">Retorna o objeto UserDto com o token</response>
        /// <response code="400">Usuário ou senha errado.</response>
        [HttpPost("login")]
        public ActionResult<UserDto> Login(UserDto userDto)
        {
            //Criar método para buscar no banco

            if (!user.Username.Equals(userDto.Username) && !user.Email.Equals(userDto.Email))
            {
                return BadRequest("Usuário não encontrado.");
            }

            if (!_passwordHashService.VerifyPasswordHash(userDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Senha errada.");
            }

            userDto = _mapper.Map<UserDto>(user);

            userDto.Token = _tokenService.CreateToken(user);
            _tokenService.SetRefreshToken(out string refreshToken, out DateTime tokenCreated, out DateTime tokenExpires);

            user.RefreshToken = refreshToken;
            user.TokenCreated = tokenCreated;
            user.TokenExpires = tokenExpires;

            return Ok(userDto);
        }

        /// <summary>
        /// Renovar token
        /// </summary>
        /// <response code="200">Objeto UserDto com o novo token</response>
        [HttpPost("refresh-token")]
        public ActionResult<UserDto> RefreshToken()
        {
            var cookieRefreshToken = Request.Cookies["refreshToken"];

            if (!user.RefreshToken.Equals(cookieRefreshToken))
            {
                return Unauthorized("Renovação de token inválida.");
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expirado.");
            }

            string token = _tokenService.CreateToken(user);
            _tokenService.SetRefreshToken(out string refreshToken, out DateTime tokenCreated, out DateTime tokenExpires);

            user.RefreshToken = refreshToken;
            user.TokenCreated = tokenCreated;
            user.TokenExpires = tokenExpires;

            UserDto userDto = _mapper.Map<UserDto>(user);
            userDto.Token = token;

            return Ok(userDto);
        }
    }
}
