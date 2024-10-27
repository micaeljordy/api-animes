using Animes.Application.DTOs.Requests;
using Animes.Application.DTOs.Responses;
using Animes.Application.Exceptions;
using Animes.Application.Interfaces;
using Animes.Application.ViewModels;
using Animes.Domain.Entities;
using Animes.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Animes.Application.Services
{
    public class AnimeService : IAnimeService
    {
        private readonly IAnimeRepository _animeRepository;
        private readonly IDiretorRepository _diretorRepository;
        private readonly ILogger<AnimeService> _logger;
        public AnimeService(IAnimeRepository animeRepository, IDiretorRepository diretorRepository, ILogger<AnimeService> logger)
        {
            _animeRepository = animeRepository;
            _diretorRepository = diretorRepository;
            _logger = logger;
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
            catch(Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar o registro de {Entity} com o ID: {Id}.", "Anime", id);
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
            catch(Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar o registro de {Entity} com os dados: {Data}.", "Anime", createAnimeRequest);
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
            catch(NotFoundException)
            {
                return false;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir o registro de {Entity} com o ID: {Id}.", "Anime", id);
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
            catch(Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar os registros de {Entity} com os dados: {Data}.", "Anime", filterAnimeRequest);
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
            catch(NotFoundException)
            {
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar o registro de {Entity} com o ID: {Id}. Dados novos: {NewData}.", "Anime", id, updateAnimeRequest);
                throw;
            }
        }
    }
}