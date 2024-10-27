using System.Text.Json;

namespace Animes.Application.DTOs.Responses
{
    public class CreateUserResponse
    {
        public string? UserName { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}