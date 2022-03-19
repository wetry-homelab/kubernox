using System;
using System.Text.Json.Serialization;

namespace Kubernox.Infrastructure.Contracts.Response
{
    public class LogItemResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("messageTemplate")]
        public string MessageTemplate { get; set; }
        [JsonPropertyName("level")]
        public string Level { get; set; }
        [JsonPropertyName("logEvent")]
        public string LogEvent { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTimeOffset TimeStamp { get; set; }

        [JsonPropertyName("exception")]
        public string Exception { get; set; }
    }
}
