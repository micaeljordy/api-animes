using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Animes.Application.Configurations;
using Animes.Application.DTOs.Requests;
using Animes.Application.Interfaces;
using Animes.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Animes.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<AuthService> _logger;
        public AuthService(IUsuarioRepository usuarioRepository, IEncryptionService encryptionService, JwtSettings jwtSettings, ILogger<AuthService> logger)
        {
            _usuarioRepository = usuarioRepository;
            _encryptionService = encryptionService;
            _jwtSettings = jwtSettings;
            _logger = logger;
        }
        public async Task<string?> Login(LoginRequest loginRequest)
        {
            try
            {
                if(await _usuarioRepository.GetUsuarioByAuth(
                    loginRequest.UserName,
                    _encryptionService.Encrypt(loginRequest.Senha)
                ) != null)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, loginRequest.UserName)
                        }),
                        Expires = DateTime.UtcNow.AddHours(_jwtSettings.Expires),
                        Issuer = _jwtSettings.Issuer,
                        Audience = _jwtSettings.Audience,
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    return "Bearer " + tokenHandler.WriteToken(token);
                }
                return null;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar o token do Usuario: {UserName}.", loginRequest.UserName);
                throw;
            }
        }
    }
}