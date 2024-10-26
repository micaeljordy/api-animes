using Animes.Application.DTOs.Responses;

namespace Animes.Application.ViewModels
{
    public class AnimeViewModel
    {
        public IEnumerable<AnimeResponseDTO>? Animes { get; set; }
    }
}