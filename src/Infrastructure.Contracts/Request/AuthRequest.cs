using System.Text.Json.Serialization;

namespace Infrastructure.Contracts.Request
{
    public class AuthRequest
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
