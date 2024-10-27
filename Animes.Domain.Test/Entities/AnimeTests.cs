using Animes.Domain.Entities;
using FluentAssertions;

namespace Animes.Domain.Test.Entities
{
    public class AnimeTests
    {
        [Fact]
        public void Should_CreateAnime_With_ValidProperties()
        {
            // Arrange
            var diretor = new Diretor { Id = 1, Nome = "Hayao Miyazaki" };
            var anime = new Anime
            {
                Id = 1,
                Nome = "Spirited Away",
                Resumo = "A young girl is trapped in a mysterious world of spirits.",
                StatusExcluido = false,
                IdDiretor = diretor.Id,
                DiretorNavigation = diretor
            };

            // Assert
            anime.Id.Should().Be(1);
            anime.Nome.Should().Be("Spirited Away");
            anime.Resumo.Should().Be("A young girl is trapped in a mysterious world of spirits.");
            anime.StatusExcluido.Should().BeFalse();
            anime.IdDiretor.Should().Be(diretor.Id);
            anime.DiretorNavigation.Should().Be(diretor);

            var diretor2 = new Diretor {Id = int.MaxValue, Nome = "宮崎駿"};
            var anime2 = new Anime
            {
                Id = int.MaxValue,
                Nome = "千と千尋の神隠し",
                Resumo = "少女は神秘的な霊の世界に閉じ込められています。",
                StatusExcluido = true,
                IdDiretor = diretor2.Id,
                DiretorNavigation = diretor2
            };

            anime2.Id.Should().Be(int.MaxValue);
            anime2.Nome.Should().Be("千と千尋の神隠し");
            anime2.Resumo.Should().Be("少女は神秘的な霊の世界に閉じ込められています。");
            anime2.StatusExcluido.Should().BeTrue();
            anime2.IdDiretor.Should().Be(diretor2.Id);
            anime2.DiretorNavigation.Should().Be(diretor2);
        }

        [Fact]
        public void Should_MarkAnimeAsDeleted_When_StatusExcluidoIsTrue()
        {
            // Arrange
            var anime = new Anime
            {
                Nome = "My Neighbor Totoro",
                StatusExcluido = true
            };

            // Assert
            anime.StatusExcluido.Should().BeTrue();

            var anime2 = new Anime
            {
                Nome = "となりのトトロ",
                StatusExcluido = true
            };

            anime.StatusExcluido.Should().BeTrue();
        }

        [Fact]
        public void Should_AssociateDirectorWithAnime()
        {
            // Arrange
            var diretor = new Diretor { Id = 2, Nome = "Mamoru Hosoda" };
            var anime = new Anime
            {
                Nome = "The Girl Who Leapt Through Time",
                IdDiretor = diretor.Id,
                DiretorNavigation = diretor
            };

            // Assert
            anime.DiretorNavigation.Should().NotBeNull();
            anime.DiretorNavigation.Nome.Should().Be("Mamoru Hosoda");
            anime.DiretorNavigation.Id.Should().Be(anime.IdDiretor);

            var diretor2 = new Diretor { Id = int.MaxValue, Nome = "細田守"};
            var anime2 = new Anime
            {
                Nome = "時をかける少女",
                IdDiretor = diretor2.Id,
                DiretorNavigation = diretor2
            };

            anime2.DiretorNavigation.Should().NotBeNull();
            anime2.DiretorNavigation.Nome.Should().Be("細田守");
            anime2.DiretorNavigation.Id.Should().Be(anime2.IdDiretor);
        }
    }
}
