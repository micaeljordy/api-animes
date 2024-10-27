using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Animes.Application.DTOs.Requests
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "O campo UserName é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O UserName deve ter no máximo 50 caracteres.")]
        [MinLength(1, ErrorMessage = "O UserName não pode ser vazio")]
        public string UserName { get; set; } = null!;
        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [MinLength(1, ErrorMessage = "O campo Senha não pode ser vazio")]
        public string Senha { get; set; } = null!;

        public override string ToString()
        {
            return JsonSerializer.Serialize(new {UserName});
        }
    }
}