using Animes.Domain.Entities;

namespace Animes.Domain.Interfaces
{
    public interface IAnimeRepository
    {
        public Task<Anime?> GetAnime(int id);
        public Task<ICollection<Anime>> GetAnimes(int skip, int take, string? criteria);
        public Task<Anime> CreateAnime(Anime anime);
        public Task<bool> UpdateAnime(Anime anime);
        public Task<bool> DeleteAnime(int id);
    }
}