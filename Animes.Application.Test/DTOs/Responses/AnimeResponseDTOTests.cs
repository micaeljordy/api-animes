using System.Text.Json;
using Animes.Application.DTOs.Responses;
using FluentAssertions;

namespace Animes.Application.Test.DTOs.Responses
{
    public class AnimeResponseDTOTests
    {
        [Fact]
        public void ToString_Should_Return_JsonRepresentation()
        {
            // Arrange
            var request = new AnimeResponseDTO
            {
                Id = int.MaxValue,
                Nome = "Evangelion",
                Resumo = "Resumo do anime",
                Diretor = "Hideaki Anno"
            };

            // Act
            var result = request.ToString();

            // Assert
            var expectedJson = JsonSerializer.Serialize(new { Nome = "Evangelion" });
            result.Should().Be(expectedJson);
        }
    }
}