using System.Text.Json.Serialization;

namespace Kubernox.Infrastructure.Contracts.Response
{
    public class TemplateListItemResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("node")]
        public int Node { get; set; }

        [JsonPropertyName("vcpu")]
        public int VCpu { get; set; }

        [JsonPropertyName("memory")]
        public int Memory { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("externalDb")]
        public bool ExternalDb { get; set; }
    }
}
