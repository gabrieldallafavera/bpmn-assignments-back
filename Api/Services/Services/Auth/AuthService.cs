﻿using Api.Database.Dtos.People;
using Api.Database.Entities.People;
using Api.Models.Email;
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
        private readonly IVerifyEmailRepository _verifyEmailRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IResetPasswordRepository _resetPasswordRepository;

        public AuthService(IMapper mapper, IHttpContextAccessor httpContextAccessor, IEmailService emailService, ITokenService tokenService, IPasswordHashService passwordHashService, IUserRepository userRepository, IVerifyEmailRepository verifyEmailRepository, IRefreshTokenRepository refreshTokenRepository, IResetPasswordRepository resetPasswordRepository)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            
            _emailService = emailService;
            _tokenService = tokenService;
            _passwordHashService = passwordHashService;
            
            _userRepository = userRepository;
            _verifyEmailRepository = verifyEmailRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _resetPasswordRepository = resetPasswordRepository;
        }

        public UserReadDto Register(UserWriteDto userWriteDto)
        {
            _passwordHashService.CreatePasswordHash(userWriteDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = _mapper.Map<User>(userWriteDto);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            user = _userRepository.Insert(user);

            _tokenService.SetVerifyEmail(out string token, out DateTime created, out DateTime expires);

            VerifyEmail verifyEmail = new VerifyEmail();

            verifyEmail.Token = token;
            verifyEmail.Expires = expires;
            verifyEmail.Created = created;
            verifyEmail.UserId = user.Id;

            EmailDto emailDto = new EmailDto
            {
                To = user.Email,
                Subject = "Confirmação de email.",
                Body = verifyEmail.Token
            };

            _emailService.SendEmail(emailDto);

            _verifyEmailRepository.Insert(verifyEmail);

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
            else if (user.VerifiedAt == null)
            {
                throw new BadHttpRequestException("Email não verificado.");
            }

            userReadDto = _mapper.Map<UserReadDto>(user);

            userReadDto.Token = _tokenService.CreateToken(user);

            _tokenService.SetRefreshToken(out string newRefreshToken, out DateTime refreshTokenCreated, out DateTime refreshTokenExpires);
            
            RefreshToken refreshToken = new RefreshToken();
            if (user.RefreshToken != null)
            {
                refreshToken = user.RefreshToken;
            }
            refreshToken.Token = newRefreshToken;
            refreshToken.Expires = refreshTokenExpires;
            refreshToken.Created = refreshTokenCreated;

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
            var cookieToken = _httpContextAccessor?.HttpContext?.Request.Cookies["refreshToken"];

            RefreshToken? refreshToken = _refreshTokenRepository.Find(refreshTokenDto.Token);

            if (refreshToken == null || refreshToken.User == null)
            {
                throw new BadHttpRequestException("Token não encontrado");
            }
            else if (!refreshToken.Token.Equals(cookieToken))
            {
                throw new UnauthorizedAccessException("Token inválido.");
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

        public void ResendVerifyEmail(UserReadDto userReadDto)
        {
            User? user = _userRepository.Find(userReadDto.Username, userReadDto.Email);

            if (user == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }
            else if (user.VerifiedAt == null)
            {
                throw new BadHttpRequestException("Email já verificado.");
            }

            _tokenService.SetVerifyEmail(out string token, out DateTime created, out DateTime expires);

            VerifyEmail verifyEmail = new VerifyEmail();
            if (user.VerifyEmail != null)
            {
                verifyEmail = user.VerifyEmail;
            }
            verifyEmail.Token = token;
            verifyEmail.Expires = expires;
            verifyEmail.Created = created;

            EmailDto emailDto = new EmailDto
            {
                To = user.Email,
                Subject = "Confirmação de email.",
                Body = verifyEmail.Token
            };

            _emailService.SendEmail(emailDto);

            if (user.ResetPassword != null)
            {
                _verifyEmailRepository.Update(verifyEmail);
            }
            else
            {
                verifyEmail.UserId = user.Id;
                _verifyEmailRepository.Insert(verifyEmail);
            }
        }

        public void VerifyEmail(string token)
        {
            var cookieToken = _httpContextAccessor?.HttpContext?.Request.Cookies["verifyEmail"];

            VerifyEmail? verifyEmail = _verifyEmailRepository.Find(token);

            if (verifyEmail == null || verifyEmail.User == null)
            {
                throw new BadHttpRequestException("Token não encontrado");
            }
            else if (!verifyEmail.Token.Equals(cookieToken))
            {
                throw new UnauthorizedAccessException("Token inválido.");
            }
            else if (verifyEmail.Expires < DateTime.Now)
            {
                throw new UnauthorizedAccessException("Token expirado.");
            }

            User user = verifyEmail.User;
            user.VerifiedAt = DateTime.Now;

            _userRepository.Update(user);
        }

        public void ForgotPassword(UserReadDto userReadDto)
        {
            User? user = _userRepository.Find(userReadDto.Username, userReadDto.Email);

            if (user == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }
            
            _tokenService.SetResetPassword(out string token, out DateTime created, out DateTime expires);

            ResetPassword resetPassword = new ResetPassword();
            if (user.ResetPassword != null)
            {
                resetPassword = user.ResetPassword;
            }
            resetPassword.Token = token;
            resetPassword.Expires = expires;
            resetPassword.Created = created;

            EmailDto emailDto = new EmailDto
            {
                To = user.Email,
                Subject = "Redefinir senha.",
                Body = resetPassword.Token
            };

            _emailService.SendEmail(emailDto);

            if (user.ResetPassword != null)
            {
                _resetPasswordRepository.Update(resetPassword);
            }
            else
            {
                resetPassword.UserId = user.Id;
                _resetPasswordRepository.Insert(resetPassword);
            }
        }

        public void ResetPassword(string token, ResetPasswordDto resetPasswordDto)
        {
            var cookieToken = _httpContextAccessor?.HttpContext?.Request.Cookies["resetPassword"];

            ResetPassword? resetPassword = _resetPasswordRepository.Find(token);

            if (resetPassword == null || resetPassword.User == null)
            {
                throw new BadHttpRequestException("Token não encontrado");
            }
            else if (!resetPassword.Token.Equals(cookieToken))
            {
                throw new UnauthorizedAccessException("Token inválido.");
            }
            else if (resetPassword.Expires < DateTime.Now)
            {
                throw new UnauthorizedAccessException("Token expirado.");
            }

            User user = resetPassword.User;

            _passwordHashService.CreatePasswordHash(resetPasswordDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _userRepository.Update(user);
        }
    }
}
