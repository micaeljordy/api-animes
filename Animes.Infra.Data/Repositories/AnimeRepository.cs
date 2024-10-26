using Animes.Domain.Entities;
using Animes.Domain.Interfaces;
using Animes.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic;


namespace Animes.Infra.Data.Repositories
{
    public class AnimeRepository : IAnimeRepository
    {
        private readonly AppDbContext _context;
        public AnimeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Anime?> GetAnime(int id)
        {
            return await _context.Animes.FindAsync(id);
        }

        public async Task<Anime> CreateAnime(Anime anime)
        {
            await _context.Animes.AddAsync(anime);
            await _context.SaveChangesAsync();
            return anime;
        }

        public async Task<bool> DeleteAnime(int id)
        {
            var anime = await _context.Animes.FirstOrDefaultAsync(p=>p.Id == id);
            if(anime == null) return false;
            anime.StatusExcluido = true;
            _context.Animes.Update(anime);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<Anime>> GetAnimes(int skip, int take, string? criteria)
        {
            var query = _context.Animes.AsQueryable();

            if (!string.IsNullOrEmpty(criteria))
            {
                query = query.Where(criteria);
            }

            return await query
                        .Include(p => p.DiretorNavigation)
                        .AsNoTracking()
                        .ToListAsync();

        }

        public async Task<bool> UpdateAnime(Anime anime)
        {
            _context.Animes.Update(anime);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}