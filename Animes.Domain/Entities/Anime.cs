namespace Animes.Domain.Entities
{
    public class Anime
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Resumo { get; set; } = null!;
        public bool StatusExcluido { get; set; }
        public int IdDiretor {get; set;}
        public Diretor DiretorNavigation {get; set;} = null!;
    }
}