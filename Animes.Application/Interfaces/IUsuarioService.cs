using Animes.Application.DTOs.Requests;
using Animes.Application.DTOs.Responses;

namespace Animes.Application.Interfaces
{
    public interface IUsuarioService
    {
        public Task<CreateUserResponse> CreateUser(CreateUserRequest createUserRequest);
        public Task<CreateUserResponse?> GetUsuarioByUserName(string username);
    }
}