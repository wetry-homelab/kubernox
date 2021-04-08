using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Infrastructure.Contracts.Request
{
    public class DomainLinkingRequestContract
    {
        [JsonPropertyName("clusterId")]
        public string ClusterId { get; set; }

        [JsonPropertyName("domainId")]
        public string DomainId { get; set; }

        [JsonPropertyName("subDomain")]
        public string SubDomain { get; set; }

        [JsonPropertyName("protocol")]
        public string Protocol { get; set; }

        [JsonPropertyName("resolver")]
        public string Resolver { get; set; }

        [JsonPropertyName("certificateFile")]
        public string CertificateFile { get; set; }

        [JsonPropertyName("keyFile")]
        public string KeyFile { get; set; }
    }
}
