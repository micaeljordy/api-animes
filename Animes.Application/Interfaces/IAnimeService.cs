using Animes.Application.ViewModels;
using Animes.Application.DTOs.Responses;
using Animes.Application.DTOs.Requests;

namespace Animes.Application.Interfaces
{
    public interface IAnimeService
    {
        public Task<AnimeResponseDTO?> GetAnime(int id);
        public Task<AnimeViewModel> GetAnimes(int? index, int? take, FilterAnimeRequest? filterAnimeRequest);
        public Task<AnimeResponseDTO> CreateAnime(CreateAnimeRequest createAnimeRequest);
        public Task<bool> UpdateAnime(int id, UpdateAnimeRequest updateAnimeRequest);
        public Task<bool> DeleteAnime(int id);
    }
}