namespace Animes.Domain.Entities
{
    public class Anime
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string? Resumo { get; set; }
        public bool StatusExcluido { get; set; }
        public int IdDiretor {get; set;}
        public virtual Diretor DiretorNavigation {get; set;} = null!;
    }
}