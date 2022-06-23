using Api.Database.Dtos.People;
using Api.Database.Entities.People;
using Api.Repositories.Interface.People;
using Api.Services.Interface.Auth;
using AutoMapper;

namespace Api.Services.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IClaimService _claimService;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHashService _passwordHashService;
        
        private readonly IUserRepository _userRepository;

        public AuthService(IMapper mapper, IHttpContextAccessor httpContextAccessor, IClaimService claimService, ITokenService tokenService, IPasswordHashService passwordHashService, IUserRepository userRepository)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;

            _claimService = claimService;
            _tokenService = tokenService;
            _passwordHashService = passwordHashService;
            
            _userRepository = userRepository;
        }

        public async Task<UserDto> Register(UserDto userDto)
        {
            _passwordHashService.CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = _mapper.Map<User>(userDto);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            user = await _userRepository.InsertAsync(user);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> Login(UserDto userDto)
        {
            //Criar método para buscar no banco
            User? user = await _userRepository.Find(userDto.Username, userDto.Email);

            if (user == null)
            {
                return BadRequest("Usuário não encontrado.");
            }
            else if (!_passwordHashService.VerifyPasswordHash(userDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Senha errada.");
            }

            userDto = _mapper.Map<UserDto>(user);

            userDto.Token = _tokenService.CreateToken(user);
            _tokenService.SetRefreshToken(out string refreshToken, out DateTime tokenCreated, out DateTime tokenExpires);

            //Separa da tabela User e salvar 
            user.RefreshToken = refreshToken;
            user.TokenCreated = tokenCreated;
            user.TokenExpires = tokenExpires;

            return userDto;
        }

        // Receber o Objeto refreshTokenDto ao invés do userDto
        public async Task<UserDto> RefreshToken(UserDto userDto)
        {
            var cookieRefreshToken = _httpContextAccessor?.HttpContext?.Request.Cookies["refreshToken"];

            User? user = await _userRepository.FindByIdAsync(userDto.Id);

            if (user == null)
            {
                return BadRequest("RefreshToken não encontrado");
            }
            else if (!user.RefreshToken.Equals(cookieRefreshToken))
            {
                return Unauthorized("Renovação de token inválida.");
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expirado.");
            }

            string token = _tokenService.CreateToken(user);
            _tokenService.SetRefreshToken(out string refreshToken, out DateTime tokenCreated, out DateTime tokenExpires);

            //Separa da tabela User e salvar 
            user.RefreshToken = refreshToken;
            user.TokenCreated = tokenCreated;
            user.TokenExpires = tokenExpires;

            userDto = _mapper.Map<UserDto>(user);
            userDto.Token = token;

            return userDto;
        }
    }
}
