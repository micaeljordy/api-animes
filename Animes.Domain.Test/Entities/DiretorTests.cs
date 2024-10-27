using Animes.Domain.Entities;
using FluentAssertions;

namespace Animes.Domain.Test.Entities
{
    public class DiretorTests
    {
        [Fact]
        public void Should_CreateDiretor_With_ValidProperties()
        {
            var ListAnime = new List<Anime>
            {
                new Anime
                {
                    Id = 1,
                    Nome = "Neon Genesis Evangelion",
                    Resumo = "Uma série sobre pilotos de mechas enfrentando seres conhecidos como 'Angels'.",
                    StatusExcluido = false,
                    IdDiretor = int.MaxValue
                },
                new Anime
                {
                    Id = 2,
                    Nome = "The End of Evangelion",
                    Resumo = "Filme que serve como o final alternativo para a série de TV.",
                    StatusExcluido = false,
                    IdDiretor = int.MaxValue
                }
            };

            var diretor = new Diretor
            {
                Nome = "Hideaki Anno",
                Id = int.MaxValue,
                AnimesNavigations = ListAnime
            };

            diretor.Id.Should().Be(int.MaxValue);
            diretor.Nome.Should().Be("Hideaki Anno");
            diretor.AnimesNavigations.Should().BeEquivalentTo(ListAnime);
        }
    }
}
