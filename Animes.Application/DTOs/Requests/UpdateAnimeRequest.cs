using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace Animes.Application.DTOs.Requests
{
    public class UpdateAnimeRequest
    {
        [SwaggerSchema(Description = "Novo nome do anime")]
        [MaxLength(250, ErrorMessage = "O Nome deve ter no máximo 250 caracteres.")]
        [MinLength(1, ErrorMessage = "O Nome não pode ser vazio")]
        public string? Nome { get; set; }
        [SwaggerSchema(Description = "Novo resumo do anime")]
        [StringLength(10000, ErrorMessage = "O campo Descricao deve ter no máximo 10000 caracteres.")]
        public string? Resumo { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}