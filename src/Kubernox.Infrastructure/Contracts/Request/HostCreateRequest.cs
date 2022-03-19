using System.Text.Json.Serialization;

namespace Kubernox.Infrastructure.Contracts.Request
{
    public class HostCreateRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("user")]
        public string User { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("ip")]
        public string Ip { get; set; }
    }
}
