using Api.Database.Entities.People;
using Api.Enums;
using Api.Models.Email;
using Api.Models.People;
using Api.Repositories.Interface.People;
using Api.Services.Interface.Auth;
using Api.Services.Interface.Email;
using AutoMapper;

namespace Api.Services.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        private readonly IEmailService _emailService;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHashService _passwordHashService;
        
        private readonly IUserRepository _userRepository;
        private readonly ITokenFunctionRepository _tokenFunctionRepository;

        public AuthService(IMapper mapper, IHttpContextAccessor httpContextAccessor, IEmailService emailService, ITokenService tokenService, IPasswordHashService passwordHashService, IUserRepository userRepository, ITokenFunctionRepository tokenFunctionRepository)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            
            _emailService = emailService;
            _tokenService = tokenService;
            _passwordHashService = passwordHashService;
            
            _userRepository = userRepository;
            _tokenFunctionRepository = tokenFunctionRepository;
        }

        public async Task<UserResponse> Register(UserRequest userRequest)
        {
            _passwordHashService.CreatePasswordHash(userRequest.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = _mapper.Map<User>(userRequest);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            List<UserRole> listUserRole = new List<UserRole>();

            if(userRequest.UserRoleRequest != null)
            {
                foreach (var item in userRequest.UserRoleRequest)
                {
                    listUserRole.Add(new UserRole { Role = item.Role });
                }
            }

            _tokenService.SetVerifyEmail(out string token, out DateTime created, out DateTime expires);

            TokenFunction tokenFunction = new TokenFunction();

            tokenFunction.Token = token;
            tokenFunction.Type = (int)TokenFunctionEnum.VerifyEmail;
            tokenFunction.Expires = expires;
            tokenFunction.Created = created;

            EmailRequest emailDto = new EmailRequest
            {
                To = user.Email,
                Subject = "Confirmação de email.",
                Body = tokenFunction.Token
            };

            _emailService.SendEmail(emailDto);

            user = await _userRepository.Insert(user, tokenFunction, listUserRole);

            return _mapper.Map<UserResponse>(user);
        }

        public async Task<UserResponse> Login(UserResponse userResponse)
        {
            User? user = await _userRepository.Find(userResponse.Username, userResponse.Email);

            if (user == null)
            {
                throw new BadHttpRequestException("Usuário não encontrado.");
            }
            else if (!_passwordHashService.VerifyPasswordHash(userResponse.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new BadHttpRequestException("Senha errada.");
            }
            else if (user.VerifiedAt == null)
            {
                throw new BadHttpRequestException("Email não verificado.");
            }

            userResponse = _mapper.Map<UserResponse>(user);

            userResponse.Token = _tokenService.CreateToken(user);

            _tokenService.SetRefreshToken(out string newRefreshToken, out DateTime refreshTokenCreated, out DateTime refreshTokenExpires);
            
            TokenFunction tokenFunction = new TokenFunction();

            var refreshToken = user.TokenFunction?.Where(x => x.Type == (int)TokenFunctionEnum.RefreshToken).FirstOrDefault();

            if (refreshToken != null)
            {
                tokenFunction = refreshToken;
            }
            tokenFunction.Token = newRefreshToken;
            tokenFunction.Type = (int)TokenFunctionEnum.RefreshToken;
            tokenFunction.Expires = refreshTokenExpires;
            tokenFunction.Created = refreshTokenCreated;

            if (refreshToken != null)
            {
                tokenFunction = await _tokenFunctionRepository.Update(tokenFunction);
            }
            else
            {
                tokenFunction.UserId = user.Id;
                tokenFunction = await _tokenFunctionRepository.Insert(tokenFunction);
            }

            userResponse.RefreshToken = tokenFunction.Token;
            userResponse.RefreshTokenExpires = tokenFunction.Expires;

            return userResponse;
        }

        public async Task<RefreshTokenResponse> RefreshToken(RefreshTokenResponse refreshTokenResponse)
        {
            var cookieToken = _httpContextAccessor?.HttpContext?.Request.Cookies["refreshToken"];

            TokenFunction? tokenFunction = await _tokenFunctionRepository.Find(refreshTokenResponse.Token, (int)TokenFunctionEnum.RefreshToken);

            if (tokenFunction == null || tokenFunction.User == null)
            {
                throw new BadHttpRequestException("Token não encontrado");
            }
            else if (!tokenFunction.Token.Equals(cookieToken))
            {
                throw new UnauthorizedAccessException("Token inválido.");
            }
            else if (tokenFunction.Expires < DateTime.Now)
            {
                throw new UnauthorizedAccessException("Token expirado.");
            }

            string token = _tokenService.CreateToken(tokenFunction.User);
            _tokenService.SetRefreshToken(out string newRefreshToken, out DateTime tokenCreated, out DateTime tokenExpires);

            tokenFunction.Token = newRefreshToken;
            tokenFunction.Expires = tokenExpires;
            tokenFunction.Created = tokenCreated;

            tokenFunction = await _tokenFunctionRepository.Update(tokenFunction);

            refreshTokenResponse = _mapper.Map<RefreshTokenResponse>(tokenFunction);
            refreshTokenResponse.NewToken = token;

            return refreshTokenResponse;
        }

        public async Task ResendVerifyEmail(UserResponse userResponse)
        {
            User? user = await _userRepository.Find(userResponse.Username, userResponse.Email);

            if (user == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }
            else if (user.VerifiedAt != null)
            {
                throw new BadHttpRequestException("Email já verificado.");
            }

            _tokenService.SetVerifyEmail(out string token, out DateTime created, out DateTime expires);

            TokenFunction tokenFunction = new TokenFunction();

            var verifyEmail = user.TokenFunction?.Where(x => x.Type == (int)TokenFunctionEnum.VerifyEmail).FirstOrDefault();

            if (verifyEmail != null)
            {
                tokenFunction = verifyEmail;
            }
            tokenFunction.Token = token;
            tokenFunction.Type = (int)TokenFunctionEnum.VerifyEmail;
            tokenFunction.Expires = expires;
            tokenFunction.Created = created;

            EmailRequest emailDto = new EmailRequest
            {
                To = user.Email,
                Subject = "Confirmação de email.",
                Body = tokenFunction.Token
            };

            _emailService.SendEmail(emailDto);

            if (verifyEmail != null)
            {
                await _tokenFunctionRepository.Update(tokenFunction);
            }
            else
            {
                tokenFunction.UserId = user.Id;
                await _tokenFunctionRepository.Insert(tokenFunction);
            }
        }

        public async Task VerifyEmail(string token)
        {
            var cookieToken = _httpContextAccessor?.HttpContext?.Request.Cookies["verifyEmail"];

            TokenFunction? tokenFunction = await _tokenFunctionRepository.Find(token, (int)TokenFunctionEnum.VerifyEmail);

            if (tokenFunction == null || tokenFunction.User == null)
            {
                throw new BadHttpRequestException("Token não encontrado");
            }
            else if (!tokenFunction.Token.Equals(cookieToken))
            {
                throw new UnauthorizedAccessException("Token inválido.");
            }
            else if (tokenFunction.Expires < DateTime.Now)
            {
                throw new UnauthorizedAccessException("Token expirado.");
            }

            User user = tokenFunction.User;
            user.VerifiedAt = DateTime.Now;

            await _userRepository.Update(user);
        }

        public async Task ForgotPassword(UserResponse userResponse)
        {
            User? user = await _userRepository.Find(userResponse.Username, userResponse.Email);

            if (user == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }
            
            _tokenService.SetResetPassword(out string token, out DateTime created, out DateTime expires);

            TokenFunction tokenFunction = new TokenFunction();

            var resetPassword = user.TokenFunction?.Where(x => x.Type == (int)TokenFunctionEnum.ResetPassword).FirstOrDefault();

            if (resetPassword != null)
            {
                tokenFunction = resetPassword;
            }
            tokenFunction.Token = token;
            tokenFunction.Type = (int)TokenFunctionEnum.ResetPassword;
            tokenFunction.Expires = expires;
            tokenFunction.Created = created;

            EmailRequest emailDto = new EmailRequest
            {
                To = user.Email,
                Subject = "Redefinir senha.",
                Body = tokenFunction.Token
            };

            _emailService.SendEmail(emailDto);

            if (resetPassword != null)
            {
                await _tokenFunctionRepository.Update(tokenFunction);
            }
            else
            {
                tokenFunction.UserId = user.Id;
                await _tokenFunctionRepository.Insert(tokenFunction);
            }
        }

        public async Task ResetPassword(string token, ResetPasswordRequest resetPasswordRequest)
        {
            var cookieToken = _httpContextAccessor?.HttpContext?.Request.Cookies["resetPassword"];

            TokenFunction? tokenFunction = await _tokenFunctionRepository.Find(token, (int)TokenFunctionEnum.ResetPassword);

            if (tokenFunction == null || tokenFunction.User == null)
            {
                throw new BadHttpRequestException("Token não encontrado");
            }
            else if (!tokenFunction.Token.Equals(cookieToken))
            {
                throw new UnauthorizedAccessException("Token inválido.");
            }
            else if (tokenFunction.Expires < DateTime.Now)
            {
                throw new UnauthorizedAccessException("Token expirado.");
            }

            User user = tokenFunction.User;

            _passwordHashService.CreatePasswordHash(resetPasswordRequest.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _userRepository.Update(user);
        }
    }
}
