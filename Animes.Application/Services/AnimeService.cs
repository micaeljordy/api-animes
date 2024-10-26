using Animes.Application.DTOs.Requests;
using Animes.Application.DTOs.Responses;
using Animes.Application.Exceptions;
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

        public async Task<AnimeResponseDTO?> GetAnime(int id)
        {
            try
            {
                var anime = await _animeRepository.GetAnime(id);
                if (anime == null)
                {
                    return null;
                }
                return new AnimeResponseDTO
                {
                    Id = anime.Id,
                    Nome = anime.Nome,
                    Resumo = anime.Resumo,
                    Diretor = anime.DiretorNavigation.Nome
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<AnimeResponseDTO> CreateAnime(CreateAnimeRequest createAnimeRequest)
        {
            try
            {
                var diretor = await _diretorRepository.GetDiretor(createAnimeRequest.Diretor.Trim());
                if (diretor == null)
                {
                    diretor = await _diretorRepository.CreateDiretor(new Diretor
                    {
                        Nome = createAnimeRequest.Diretor.Trim()
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
                var success = await _animeRepository.DeleteAnime(id);
                if (!success)
                {
                    throw new NotFoundException("Anime não encontrado!");
                }
                return success;
            }
            catch
            {
                throw;
            }
        }

        public async Task<AnimeViewModel> GetAnimes(int? index, int? take, FilterAnimeRequest? filterAnimeRequest)
        {
            try
            {
                int skip = 0;
                if (!take.HasValue)
                {
                    take = int.MaxValue;
                }
                else
                {
                    if (take.Value < int.MaxValue && index.HasValue)
                    {
                        skip = take.Value * index.Value;
                    }
                }

                string criteria = string.Empty;
                if (filterAnimeRequest != null)
                {
                    // Monta os critérios de forma segura
                    if (!string.IsNullOrEmpty(filterAnimeRequest.Nome))
                    {
                        criteria = $"Nome.ToLower().Contains(\"{filterAnimeRequest.Nome.Trim().ToLower()}\")";
                    }
                    if (!string.IsNullOrEmpty(filterAnimeRequest.Resumo))
                    {
                        if (!string.IsNullOrEmpty(criteria)) criteria += " && ";
                        criteria += $"Resumo.ToLower().Contains(\"{filterAnimeRequest.Resumo.Trim().ToLower()}\")";
                    }
                    if (!string.IsNullOrEmpty(filterAnimeRequest.Diretor))
                    {
                        if (!string.IsNullOrEmpty(criteria)) criteria += " && ";
                        criteria += $"DiretorNavigation.Nome.ToLower().Contains(\"{filterAnimeRequest.Diretor.Trim().ToLower()}\")";
                    }
                }

                // Chama o repositório com o critério
                var animes = await _animeRepository.GetAnimes(skip, take.Value, criteria);

                // Retorna a ViewModel com os dados
                return new AnimeViewModel(animes);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> UpdateAnime(int id, UpdateAnimeRequest updateAnimeRequest)
        {
            try
            {
                var anime = await _animeRepository.GetAnime(id);
                if (anime == null)
                {
                    throw new NotFoundException("Anime não encontrado!");
                }
                anime.Nome = updateAnimeRequest.Nome?.Trim() ?? anime.Nome;
                anime.Resumo = updateAnimeRequest.Resumo?.Trim() ?? anime.Resumo;
                return await _animeRepository.UpdateAnime(anime);
            }
            catch
            {
                throw;
            }
        }
    }
}