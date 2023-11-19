using System.Text.Json.Serialization;

namespace Kubernox.Shared.Contracts.Request
{
    public class CreateProjectRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
