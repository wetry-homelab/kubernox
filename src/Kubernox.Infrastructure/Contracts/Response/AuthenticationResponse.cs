using System.Text.Json.Serialization;

namespace Kubernox.Infrastructure.Contracts.Response
{
    public class AuthenticationResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("passwordToken")]
        public string PasswordToken { get; set; }

        [JsonPropertyName("passwordExpire")]
        public bool PasswordExpire { get; set; }
    }
}
