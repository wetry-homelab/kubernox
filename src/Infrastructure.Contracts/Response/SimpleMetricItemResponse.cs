using System;
using System.Text.Json.Serialization;

namespace Infrastructure.Contracts.Response
{
    public class SimpleMetricItemResponse
    {
        [JsonPropertyName("cpuValue")]
        public long CpuValue { get; set; }

        [JsonPropertyName("ramValue")]
        public long RamValue { get; set; }

        [JsonPropertyName("dateValue")]
        public DateTime DateValue { get; set; }
    }
}
