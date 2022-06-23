using AutoMapper;
using Domain.Dtos.People;
using Domain.Entities.People;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services.Interface.People;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Api.Controllers
{
    [ApiController]
    [Route("autentication")]
    [Produces("application/json")]
    [ApiExplorerSettings(GroupName = "Autenticação")]
    public class AuthController : ControllerBase
    {
        public static User user = new User(); // Verificar para subistituir pela consulta do banco
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        //Converter de IUserService para services necessário para reduzir a controller
        public AuthController(IConfiguration configuration, IMapper mapper, IUserService userService)
        {
            _configuration = configuration;
            _mapper = mapper;
            
            _userService = userService;
        }

        [HttpGet, Authorize]
        public ActionResult<object> GetClaims()
        {
            return Ok(new
            {
                nameIdentifier = User.FindFirstValue(ClaimTypes.NameIdentifier),
                name = User.FindFirstValue(ClaimTypes.Name),
                email = User.FindFirstValue(ClaimTypes.Email),
                role = User.FindFirstValue(ClaimTypes.Role)
            });
        }

        /// <summary>
        /// Registra um novo usuário
        /// </summary>
        /// <param name="userDto">Objeto UserDto</param>
        /// <response code="200">Retorno o objeto UserDto criado</response>
        [HttpPost("register"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDto>> Register(UserDto userDto)
        {
            CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

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
        public async Task<ActionResult<UserDto>> Login(UserDto userDto)
        {
            //Criar método para buscar no banco

            if (!user.Username.Equals(userDto.Username) && !user.Email.Equals(userDto.Email))
            {
                return BadRequest("Usuário não encontrado.");
            }

            if (!VerifyPasswordHash(userDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Senha errada.");
            }

            userDto = _mapper.Map<UserDto>(user);

            userDto.Token = CreateToken(user);

            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken);

            return Ok(userDto);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<UserDto>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (!user.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Renovação de token inválida.");
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expirado.");
            }

            string token = CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken);

            UserDto userDto = _mapper.Map<UserDto>(user);
            userDto.Token = token;

            return Ok(userDto);
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        private void SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
