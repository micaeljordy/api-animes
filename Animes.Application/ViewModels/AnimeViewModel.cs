using Animes.Application.DTOs.Responses;
using Animes.Domain.Entities;

namespace Animes.Application.ViewModels
{
    public class AnimeViewModel
    {
        public IEnumerable<AnimeResponseDTO>? Animes { get; set; }
        public AnimeViewModel(IEnumerable<Anime> listOfAnimes)
        {
            Animes = listOfAnimes.Select(p => new AnimeResponseDTO
            {
                Id = p.Id,
                Nome = p.Nome,
                Resumo = p.Resumo,
                Diretor = p.DiretorNavigation.Nome
            });
        }
    }
}