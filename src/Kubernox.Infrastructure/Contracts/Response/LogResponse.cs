using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Kubernox.Infrastructure.Contracts.Response
{
    public class LogResponse
    {
        [JsonPropertyName("itemCount")]
        public int ItemCount { get; set; }

        [JsonPropertyName("logs")]
        public IEnumerable<LogItemResponse> Logs { get; set; }
    }
}
