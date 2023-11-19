using System.Text.Json.Serialization;

namespace Kubernox.Shared.Contracts.Response
{
    public class NodeItemResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
