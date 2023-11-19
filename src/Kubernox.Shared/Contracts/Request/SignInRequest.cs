using System.Text.Json.Serialization;

namespace Kubernox.Shared.Contracts.Request
{
    public class SignInRequest
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
