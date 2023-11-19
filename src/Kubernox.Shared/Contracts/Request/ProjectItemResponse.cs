using System.Text.Json.Serialization;

namespace Kubernox.Shared.Contracts.Request
{
    public class ProjectItemResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("identifier")]
        public string Identifier { get; set; }
    }
}
