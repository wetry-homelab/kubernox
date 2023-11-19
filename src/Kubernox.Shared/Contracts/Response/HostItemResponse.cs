using System.Text.Json.Serialization;

namespace Kubernox.Shared.Contracts.Response
{
    public class HostItemResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("ip")]
        public string Ip { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("nodeCount")]
        public int NodeCount { get; set; }

        [JsonPropertyName("nodes")]
        public List<NodeItemResponse> Nodes { get; set; }
    }
}
