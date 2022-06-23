using AutoMapper;
using Domain.Dtos.People;
using Domain.Entities.People;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Api.Controllers
{
    [Route("autentication")]
    [Produces("application/json")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User(); // Verificar para subistituir pela consulta do banco
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthController(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
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

            return Ok(_mapper.Map<UserDto>(User));
        }

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

            return Ok(userDto);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Name),
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
