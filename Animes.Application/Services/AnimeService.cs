using Animes.Application.DTOs.Requests;
using Animes.Application.DTOs.Responses;
using Animes.Application.Interfaces;
using Animes.Application.ViewModels;
using Animes.Domain.Entities;
using Animes.Domain.Interfaces;

namespace Animes.Application.Services
{
    public class AnimeService : IAnimeService
    {
        private readonly IAnimeRepository _animeRepository;
        private readonly IDiretorRepository _diretorRepository;
        public AnimeService(IAnimeRepository animeRepository, IDiretorRepository diretorRepository)
        {
            _animeRepository = animeRepository;
            _diretorRepository = diretorRepository;
        }

        public async Task<AnimeResponseDTO> CreateAnime(CreateAnimeRequest createAnimeRequest)
        {
            try
            {
                var diretor = await _diretorRepository.GetDiretor(createAnimeRequest.Diretor.Trim());
                if(diretor == null)
                {
                    diretor = await _diretorRepository.CreateDiretor(new Diretor
                    {
                        Nome = createAnimeRequest.Nome.Trim()
                    });
                }
                var anime = new Anime
                {
                    Nome = createAnimeRequest.Nome.Trim(),
                    Resumo = createAnimeRequest.Resumo?.Trim(),
                    IdDiretor = diretor.Id
                };
                await _animeRepository.CreateAnime(anime);

                return new AnimeResponseDTO
                {
                    Id = anime.Id,
                    Nome = anime.Nome,
                    Resumo = anime.Resumo,
                    Diretor = diretor.Nome
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteAnime(int id)
        {
            try
            {
                return await _animeRepository.DeleteAnime(id);
            }
            catch
            {
                throw;
            }
        }

        public Task<AnimeViewModel> GetAnimes(int? index, int? take, FilterAnimeRequest? filterAnimeRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAnime(int id, UpdateAnimeRequest updateAnimeRequest)
        {
            try
            {
                var anime = await _animeRepository.GetAnime(id);
                if(anime == null)
                {
                    return false;
                }
                anime.Nome = updateAnimeRequest.Nome?.Trim()?? anime.Nome;
                anime.Resumo = updateAnimeRequest.Resumo?.Trim()?? anime.Resumo;
                return await _animeRepository.UpdateAnime(anime);
            }
            catch
            {
                throw;
            }
        }
    }
}