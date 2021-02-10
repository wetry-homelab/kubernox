using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Infrastructure.Contracts.Response
{
    public class ClusterItemResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
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
        public List<ClusterNodeItemResponse> Nodes { get; set; }
    }
}
