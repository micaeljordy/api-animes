using System.Text.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace Animes.Application.DTOs.Responses
{
    public class CreateUserResponse
    {
        [SwaggerSchema(Description = "Username do usuário")]
        public string? UserName { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}