using System.Text.Json.Serialization;

namespace Kubernox.Shared.Contracts.Response
{
    public class SignInResponse
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }
    }
}
