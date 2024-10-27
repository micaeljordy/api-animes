namespace Animes.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Senha { get; set; } = null!;

    }
}