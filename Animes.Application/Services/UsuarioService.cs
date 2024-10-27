using System.Text;
using Animes.Application.DTOs.Requests;
using Animes.Application.DTOs.Responses;
using Animes.Application.Exceptions;
using Animes.Application.Interfaces;
using Animes.Domain.Entities;
using Animes.Domain.Interfaces;

namespace Animes.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEncryptionService _encryptionService;
        public UsuarioService(IUsuarioRepository usuarioRepository, IEncryptionService encryptionService)
        {
            _usuarioRepository = usuarioRepository;
            _encryptionService = encryptionService;
        }
        private string GenerateRandomString()
        {
            int length = new Random().Next(1, 10);
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder stringBuilder = new StringBuilder();
            Random random = new Random();

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
                                .Split(" ");
                while(nomesSplit.Length < 2)
                {
                    nomesSplit.Append(GenerateRandomString());
                }
                if(nomesSplit.First().Length > 48)
                {
                    throw new BusinessRulesException("Não é possível criar um UserName válido, tente cadastrar outro Nome.");
                }
                var username = nomesSplit.First() + '.' + nomesSplit.Last();
                while(username.Length > 50 && (await _usuarioRepository.GetUsuarioByUserName(username)) != null)
                {
                    nomesSplit.Append(GenerateRandomString());
                    username = nomesSplit.First() + '.' + nomesSplit.Last();
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
            catch
            {
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
            catch
            {
                throw;
            }
        }
    }
}