using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Animes.Application.DTOs.Requests;
using FluentAssertions;

namespace Animes.Application.Test.DTOs.Requests
{
    public class LoginRequestTests
    {
        [Fact]
        public void Propiedades_DevemSerValidas_QuandoNosLimites()
        {
            var request = new LoginRequest
            {
                UserName = "Marcos",
                Senha = "teste123"
            };
            var validationContext = new ValidationContext(request);
            var validationResults = new List<ValidationResult>();
            
            Validator.TryValidateObject(request, validationContext, validationResults, true);

            Assert.Empty(validationResults);
        }

        [Fact]
        public void Propiedades_DevemSerInvalidas_QuandoForaDosLimites()
        {
            var request = new LoginRequest
            {
                UserName = new string('a', 10001),
                Senha = string.Empty
            };

            var validationContext = new ValidationContext(request);
            var validationResults = new List<ValidationResult>();

            Validator.TryValidateObject(request, validationContext, validationResults, true);

            validationResults.Count.Should().Be(2);

            var request2 = new LoginRequest
            {
                UserName = string.Empty,
                Senha = string.Empty
            };

            var validationContext2 = new ValidationContext(request2);
            var validationResults2 = new List<ValidationResult>();

            Validator.TryValidateObject(request2, validationContext2, validationResults2, true);

            validationResults2.Count.Should().Be(2);
        }

        [Fact]
        public void ToString_Should_Return_JsonRepresentation()
        {
            // Arrange
            var request = new LoginRequest
            {
                UserName = "Marcos",
                Senha = "teste123"
            };

            // Act
            var result = request.ToString();

            // Assert
            var expectedJson = JsonSerializer.Serialize(new { UserName = "Marcos" });
            result.Should().Be(expectedJson);
        }
    }
}