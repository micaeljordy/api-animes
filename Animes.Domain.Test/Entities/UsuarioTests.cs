using Animes.Domain.Entities;
using FluentAssertions;

namespace Animes.Domain.Test.Entities
{
    public class UsuarioTests
    {
        [Fact]
        public void Should_CreateUsuario_With_ValidProperties()
        {
            var usuario = new Usuario
            {
                Id = int.MaxValue,
                Nome = "Pedro de Alcântara Francisco Antônio João Carlos Xavier de Paula Miguel Rafael Joaquim José Gonzaga Pascoal Cipriano Serafim de Bragança e Bourbon",
                UserName = "pedro.bourbon",
                Senha = "aa1bf4646de67fd9086cf6c79007026c"
            };

            usuario.Id.Should().Be(int.MaxValue);
            usuario.Nome.Should().Be("Pedro de Alcântara Francisco Antônio João Carlos Xavier de Paula Miguel Rafael Joaquim José Gonzaga Pascoal Cipriano Serafim de Bragança e Bourbon");
            usuario.UserName.Should().Be("pedro.bourbon");
            usuario.Senha.Should().Be("aa1bf4646de67fd9086cf6c79007026c");
        }
    }
}