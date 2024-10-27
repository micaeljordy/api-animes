using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Animes.Application.DTOs.Requests
{
    public class CreateAnimeRequest
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [MaxLength(250, ErrorMessage = "O Nome deve ter no máximo 250 caracteres.")]
        [MinLength(1, ErrorMessage = "O Nome não pode ser vazio")]
        public string Nome { get; set; } = null!;
        [MaxLength(10000, ErrorMessage = "O campo Descricao deve ter no máximo 10000 caracteres.")]
        public string? Resumo { get; set; }
        [Required(ErrorMessage = "O campo Diretor é obrigatório.")]
        [MaxLength(250, ErrorMessage = "O campo Diretor deve ter no máximo 250 caracteres.")]
        [MinLength(1, ErrorMessage = "O campo Diretor não pode ser vazio")]
        public string Diretor { get; set; } = null!;

        public override string ToString()
        {
            return JsonSerializer.Serialize(new {Nome});
        }
    }
}