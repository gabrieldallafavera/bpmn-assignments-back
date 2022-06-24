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

        private readonly ITokenService _tokenService;
        private readonly IPasswordHashService _passwordHashService;
        
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthService(IMapper mapper, IHttpContextAccessor httpContextAccessor, ITokenService tokenService, IPasswordHashService passwordHashService, IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;

            _tokenService = tokenService;
            _passwordHashService = passwordHashService;
            
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<UserReadDto> Register(UserWriteDto userWriteDto)
        {
            _passwordHashService.CreatePasswordHash(userWriteDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = _mapper.Map<User>(userWriteDto);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            user = await _userRepository.InsertAsync(user);

            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<UserReadDto> Login(UserReadDto userReadDto)
        {
            User? user = await _userRepository.Find(userReadDto.Username, userReadDto.Email);

            if (user == null)
            {
                throw new BadHttpRequestException("Usuário não encontrado.");
            }
            else if (!_passwordHashService.VerifyPasswordHash(userReadDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new BadHttpRequestException("Senha errada.");
            }

            userReadDto = _mapper.Map<UserReadDto>(user);

            userReadDto.Token = _tokenService.CreateToken(user);
            _tokenService.SetRefreshToken(out string newRefreshToken, out DateTime tokenCreated, out DateTime tokenExpires);

            RefreshToken refreshToken = new RefreshToken();
            if (user.RefreshToken != null)
            {
                refreshToken = user.RefreshToken;
            }
            refreshToken.Token = newRefreshToken;
            refreshToken.Expires = tokenExpires;
            refreshToken.Created = tokenCreated;

            if (user.RefreshToken != null)
            {
                refreshToken = await _refreshTokenRepository.UpdateAsync(refreshToken);
            }
            else
            {
                refreshToken.UserId = user.Id;
                refreshToken = await _refreshTokenRepository.InsertAsync(refreshToken);
            }

            userReadDto.RefreshToken = refreshToken.Token;
            userReadDto.RefreshTokenExpires = refreshToken.Expires;

            return userReadDto;
        }

        public async Task<RefreshTokenDto> RefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var cookieRefreshToken = _httpContextAccessor?.HttpContext?.Request.Cookies["refreshToken"];

            RefreshToken? refreshToken = await _refreshTokenRepository.Find(refreshTokenDto.Token);

            if (refreshToken == null || refreshToken.User == null)
            {
                throw new BadHttpRequestException("RefreshToken não encontrado");
            }
            else if (!refreshToken.Token.Equals(cookieRefreshToken))
            {
                throw new UnauthorizedAccessException("Renovação de token inválida.");
            }
            else if (refreshToken.Expires < DateTime.Now)
            {
                throw new UnauthorizedAccessException("Token expirado.");
            }

            string token = _tokenService.CreateToken(refreshToken.User);
            _tokenService.SetRefreshToken(out string newRefreshToken, out DateTime tokenCreated, out DateTime tokenExpires);

            refreshToken.Token = newRefreshToken;
            refreshToken.Expires = tokenExpires;
            refreshToken.Created = tokenCreated;

            refreshToken = await _refreshTokenRepository.UpdateAsync(refreshToken);

            refreshTokenDto = _mapper.Map<RefreshTokenDto>(refreshToken);
            refreshTokenDto.NewToken = token;

            return refreshTokenDto;
        }
    }
}
