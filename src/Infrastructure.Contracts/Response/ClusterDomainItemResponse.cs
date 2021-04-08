using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.Contracts.Response
{
    public class ClusterDomainItemResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("rootDomainId")]
        public string RootDomainId { get; set; }

        [JsonPropertyName("clusterId")]
        public string ClusterId { get; set; }

        [JsonPropertyName("resolver")]
        public string Resolver { get; set; }

        [JsonPropertyName("protocol")]
        public string Protocol { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public DateTime? DeleteAt { get; set; }
    }
}
