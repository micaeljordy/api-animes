using Animes.Domain.Entities;

namespace Animes.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        public Task<Usuario> CreateUsuario(Usuario usuario);
        public Task<Usuario?> GetUsuarioByAuth(string username, string senha);
        public Task<Usuario?> GetUsuarioByUserName(string userName);  
        public Task<Usuario?> GetUsuarioById(int id);
    }
}