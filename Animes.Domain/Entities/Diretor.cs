namespace Animes.Domain.Entities
{
    public class Diretor
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public ICollection<Anime> AnimesNavigations {get; set;} = new List<Anime>();
    }
}