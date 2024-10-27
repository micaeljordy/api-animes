using System.ComponentModel.DataAnnotations;

namespace Animes.Application.DTOs.Requests
{
    public class FilterAnimeRequest
    {
        [StringLength(250, ErrorMessage = "O Nome deve ter no máximo 250 caracteres.")]
        [MinLength(1, ErrorMessage = "O Nome não pode ser vazio")]
        public string? Nome { get; set; }
        [StringLength(10000, ErrorMessage = "O campo Descricao deve ter no máximo 10000 caracteres.")]
        [MinLength(1, ErrorMessage = "O campo Resumo não pode ser vazio")]
        public string? Resumo { get; set; }
        [StringLength(250, ErrorMessage = "O campo Diretor deve ter no máximo 250 caracteres.")]
        [MinLength(1, ErrorMessage = "O campo Diretor não pode ser vazio")]
        public string? Diretor { get; set; }
    }
}