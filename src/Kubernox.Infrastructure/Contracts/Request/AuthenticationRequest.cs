using System.Text.Json.Serialization;

namespace Kubernox.Infrastructure.Contracts.Request
{
    public class AuthenticationRequest
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
