using System.Text.Json.Serialization;

namespace Kubernox.Shared.Contracts.Response
{
    public class SshKeyItemResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("publicKey")]
        public string PublicKey { get; set; }

        [JsonPropertyName("privateKey")]
        public string PrivateKey { get; set; }
    }
}
