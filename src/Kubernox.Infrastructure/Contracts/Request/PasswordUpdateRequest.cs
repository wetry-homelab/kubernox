using System;
using System.Text.Json.Serialization;

namespace Kubernox.Infrastructure.Contracts.Request
{
    public class PasswordUpdateRequest
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("passwordToken")]
        public string PasswordToken { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("repeatedPassword")]
        public string RepeatedPassword { get; set; }
    }
}
