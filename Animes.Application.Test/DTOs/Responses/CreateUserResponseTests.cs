using System.Text.Json;
using Animes.Application.DTOs.Responses;
using FluentAssertions;

namespace Animes.Application.Test.DTOs.Responses
{
    public class CreateUserResponseTests
    {
        [Fact]
        public void ToString_Should_Return_JsonRepresentation()
        {
            // Arrange
            var request = new CreateUserResponse
            {
                UserName = "pedro.silva"
            };

            // Act
            var result = request.ToString();

            // Assert
            var expectedJson = JsonSerializer.Serialize(request);
            result.Should().Be(expectedJson);
        }
    }
}