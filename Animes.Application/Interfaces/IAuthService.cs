using Animes.Application.DTOs.Requests;

namespace Animes.Application.Interfaces
{
    public interface IAuthService
    {
        public Task<string?> Login(LoginRequest loginRequest);
    }
}