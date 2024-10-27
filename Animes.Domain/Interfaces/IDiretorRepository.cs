using Animes.Domain.Entities;

namespace Animes.Domain.Interfaces
{
    public interface IDiretorRepository
    {
        public Task<Diretor?> GetDiretor(string Nome);
        public Task<bool> UpdateDiretor(Diretor diretor);
        public Task<Diretor> CreateDiretor(Diretor diretor);
    }
}