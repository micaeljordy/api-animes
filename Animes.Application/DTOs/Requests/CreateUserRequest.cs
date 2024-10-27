using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Animes.Application.DTOs.Requests
{
    public class CreateUserRequest
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [MaxLength(250, ErrorMessage = "O Nome deve ter no máximo 250 caracteres.")]
        [MinLength(1, ErrorMessage = "O Nome não pode ser vazio")]
        public string Nome { get; set; } = null!;
        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [MinLength(1, ErrorMessage = "O campo Senha não pode ser vazio")]
        public string Senha { get; set; } = null!;
        public override string ToString()
        {
            return JsonSerializer.Serialize(new {Nome});
        }
    }
}