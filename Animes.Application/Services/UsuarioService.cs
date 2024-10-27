using System.Text;
using Animes.Application.DTOs.Requests;
using Animes.Application.DTOs.Responses;
using Animes.Application.Exceptions;
using Animes.Application.Interfaces;
using Animes.Domain.Entities;
using Animes.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Animes.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly ILogger<UsuarioService> _logger;
        private static readonly Random random = new Random();
        public UsuarioService(IUsuarioRepository usuarioRepository, IEncryptionService encryptionService, ILogger<UsuarioService> logger)
        {
            _usuarioRepository = usuarioRepository;
            _encryptionService = encryptionService;
            _logger = logger;
        }
        private string GenerateRandomString()
        {
            int length = random.Next(1, 10);
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                char randomChar = chars[random.Next(chars.Length)];
                stringBuilder.Append(randomChar);
            }

            return stringBuilder.ToString();
        }
        public async Task<CreateUserResponse> CreateUser(CreateUserRequest createUserRequest)
        {
            try
            {
                var nomesSplit = createUserRequest.Nome
                        .ToLower()
                        .Trim()
                        .Split(" ")
                        .ToList();
                while (nomesSplit.Count < 2)
                {
                    nomesSplit.Add(GenerateRandomString());
                }
                if (nomesSplit.First().Length > 48)
                {
                    throw new BusinessRulesException("Não é possível criar um UserName válido, tente abreviar.");
                }
                var username = nomesSplit.First() + '.' + nomesSplit.Last();
                while (username.Length > 50 && (await _usuarioRepository.GetUsuarioByUserName(username)) != null)
                {
                    nomesSplit.Append(GenerateRandomString());
                    username = nomesSplit.First() + '.' + nomesSplit.Last();

                    // Adicione uma condição de saída para evitar um loop infinito
                    if (nomesSplit.Count > 100) // Limite de tentativas
                    {
                        throw new BusinessRulesException("Não foi possível gerar um UserName único.");
                    }
                }

                await _usuarioRepository.CreateUsuario(new Usuario
                {
                    Nome = createUserRequest.Nome,
                    UserName = username,
                    Senha = _encryptionService.Encrypt(createUserRequest.Senha)
                });

                return new CreateUserResponse
                {
                    UserName = username
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar o registro de {Entity} com os dados: {Data}.", "Usuario", createUserRequest);
                throw;
            }
        }
        public async Task<CreateUserResponse?> GetUsuarioByUserName(string username)
        {
            try
            {
                var user = await _usuarioRepository.GetUsuarioByUserName(username);
                if (user == null)
                {
                    return null;
                }
                return new CreateUserResponse
                {
                    UserName = user.UserName
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar o registro de {Entity} com o UserName: {UserName}.", "Usuário", username);
                throw;
            }
        }
    }
}