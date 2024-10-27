using Animes.Application.DTOs.Responses;
using Animes.Domain.Entities;
using System.Text.Json;

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

        public override string ToString()
        {
            return String.Join(
                Environment.NewLine, 
                JsonSerializer.Serialize(Animes?.Select(p=>p.Nome))
                );
        }
    }
}