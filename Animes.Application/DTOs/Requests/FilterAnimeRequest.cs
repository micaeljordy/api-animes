using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Animes.Application.DTOs.Requests
{
    public class FilterAnimeRequest
    {
        [MaxLength(25, ErrorMessage = "O Nome deve ter no máximo 25 caracteres.")]
        [MinLength(1, ErrorMessage = "O Nome não pode ser vazio")]
        public string? Nome { get; set; }
        [MaxLength(25, ErrorMessage = "O campo Descricao deve ter no máximo 25 caracteres.")]
        [MinLength(1, ErrorMessage = "O campo Resumo não pode ser vazio")]
        public string? Resumo { get; set; }
        [MaxLength(25, ErrorMessage = "O campo Diretor deve ter no máximo 25 caracteres.")]
        [MinLength(1, ErrorMessage = "O campo Diretor não pode ser vazio")]
        public string? Diretor { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}