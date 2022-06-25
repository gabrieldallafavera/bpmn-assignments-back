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

        public UserReadDto Register(UserWriteDto userWriteDto)
        {
            _passwordHashService.CreatePasswordHash(userWriteDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = _mapper.Map<User>(userWriteDto);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            user = _userRepository.Insert(user);

            return _mapper.Map<UserReadDto>(user);
        }

        public UserReadDto Login(UserReadDto userReadDto)
        {
            User? user = _userRepository.Find(userReadDto.Username, userReadDto.Email);

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
                refreshToken = _refreshTokenRepository.Update(refreshToken);
            }
            else
            {
                refreshToken.UserId = user.Id;
                refreshToken = _refreshTokenRepository.Insert(refreshToken);
            }

            userReadDto.RefreshToken = refreshToken.Token;
            userReadDto.RefreshTokenExpires = refreshToken.Expires;

            return userReadDto;
        }

        public RefreshTokenDto RefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var cookieRefreshToken = _httpContextAccessor?.HttpContext?.Request.Cookies["refreshToken"];

            RefreshToken? refreshToken = _refreshTokenRepository.Find(refreshTokenDto.Token);

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

            refreshToken = _refreshTokenRepository.Update(refreshToken);

            refreshTokenDto = _mapper.Map<RefreshTokenDto>(refreshToken);
            refreshTokenDto.NewToken = token;

            return refreshTokenDto;
        }
    }
}
