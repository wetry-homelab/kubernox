using System;
using System.Text.Json.Serialization;

namespace Infrastructure.Contracts.Response
{
    public class DomainNameItemResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("rootDomain")]
        public string RootDomain { get; set; }

        [JsonPropertyName("validationKey")]
        public string ValidationKey { get; set; }
        
        [JsonPropertyName("validationDate")]
        public DateTime? ValidationDate { get; set; }

        public string SubDomainCount { get; set; }
    }
}
