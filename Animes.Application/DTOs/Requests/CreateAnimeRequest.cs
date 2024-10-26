using System.ComponentModel.DataAnnotations;

namespace Animes.Application.DTOs.Requests
{
    public class CreateAnimeRequest
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(250, ErrorMessage = "O Nome deve ter no máximo 250 caracteres.")]
        public string Nome { get; set; } = null!;
        [StringLength(10000, ErrorMessage = "O campo Descricao deve ter no máximo 10000 caracteres.")]
        public string? Resumo { get; set; }
        [Required(ErrorMessage = "O campo Diretor é obrigatório.")]
        [StringLength(250, ErrorMessage = "O campo Diretor deve ter no máximo 250 caracteres.")]
        public string Diretor { get; set; } = null!;
    }
}