using System.Text.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace Animes.Application.DTOs.Responses
{
    public class AnimeResponseDTO
    {
        [SwaggerSchema(Description = "ID do anime")]
        public int Id { get; set; }
        [SwaggerSchema(Description = "Nome do anime")]
        public string? Nome { get; set; }
        [SwaggerSchema(Description = "Resumo/Descrição do anime")]
        public string? Resumo { get; set; }
        [SwaggerSchema(Description = "Nome do diretor")]
        public string? Diretor { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(new {Nome});
        }
    }
}