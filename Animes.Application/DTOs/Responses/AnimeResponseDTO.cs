using System.Text.Json;

namespace Animes.Application.DTOs.Responses
{
    public class AnimeResponseDTO
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Resumo { get; set; }
        public string? Diretor { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(new {Nome});
        }
    }
}