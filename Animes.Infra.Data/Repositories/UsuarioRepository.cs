using Animes.Domain.Entities;
using Animes.Domain.Interfaces;
using Animes.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Animes.Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public readonly AppDbContext _context;
        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Usuario> CreateUsuario(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }
        public async Task<Usuario?> GetUsuarioByAuth(string username, string senha)
        {
            var usuario = await _context.Usuarios
                                .FirstOrDefaultAsync(p=>p.UserName == username && p.Senha == senha);
            return usuario;
        }
        public async Task<Usuario?> GetUsuarioByUserName(string userName)
        {
            var usuario = await _context.Usuarios
                                .FirstOrDefaultAsync(p=>p.UserName == userName);
            return usuario;
        }
        public async Task<Usuario?> GetUsuarioById(int id)
        {
            var usuario = await _context.Usuarios
                                .FirstOrDefaultAsync(p=>p.Id == id);
            return usuario;
        }
    }
}