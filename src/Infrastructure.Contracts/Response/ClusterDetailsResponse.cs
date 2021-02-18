using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Infrastructure.Contracts.Response
{
    public class ClusterDetailsResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("ip")]
        public string Ip { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("cpu")]
        public int Cpu { get; set; }

        [JsonPropertyName("memory")]
        public int Memory { get; set; }

        [JsonPropertyName("disk")]
        public int Disk { get; set; }

        [JsonPropertyName("nodes")]
        public List<ClusterNodeDetailsResponse> Nodes { get; set; }

        [JsonPropertyName("masterMetrics")]
        public List<SimpleMetricItemResponse> MasterMetrics { get; set; }
    }
}
