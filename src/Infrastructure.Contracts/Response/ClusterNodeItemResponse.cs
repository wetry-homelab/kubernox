using System.Text.Json.Serialization;

namespace Infrastructure.Contracts.Response
{
    public class ClusterNodeItemResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("ip")]
        public string Ip { get; set; }
        [JsonPropertyName("state")]
        public string State { get; set; }
    }
}
