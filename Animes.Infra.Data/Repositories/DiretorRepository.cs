using Animes.Domain.Entities;
using Animes.Domain.Interfaces;
using Animes.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Animes.Infra.Data.Repositories
{
    public class DiretorRepository : IDiretorRepository
    {
        private readonly AppDbContext _context;
        public DiretorRepository (AppDbContext context)
        {
            _context = context;
        }
        public async Task<Diretor?> GetDiretor(string nome)
        {
            return await _context.Diretores
                        .AsNoTracking()
                        .FirstOrDefaultAsync(p=>p.Nome.ToUpper() == nome.ToUpper());
        }
        public async Task<Diretor> CreateDiretor(Diretor diretor)
        {
            await _context.AddAsync(diretor);
            await _context.SaveChangesAsync();
            return diretor;
        }
        public async Task<bool> UpdateDiretor(Diretor diretor)
        {
            _context.Diretores.Update(diretor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}