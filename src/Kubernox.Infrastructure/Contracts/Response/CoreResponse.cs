using System.Text.Json.Serialization;

namespace Kubernox.Infrastructure.Contracts.Response
{
    public class CoreResponse<T>
    {
        public CoreResponse(bool success, T data)
        {
            Data = data;
            Success = success;
        }

        public CoreResponse(bool success, dynamic additionalData, T data)
        {
            AdditionalData = additionalData;
            Data = data;
            Success = success;
        }

        public CoreResponse(bool success, dynamic error, dynamic errorMessage)
        {
            Success = success;
            Error = error;
            ErrorMessage = errorMessage;
        }

        public CoreResponse(bool success, dynamic error, dynamic errorMessage, dynamic additionalData)
        {
            Success = success;
            Error = error;
            ErrorMessage = errorMessage;
            AdditionalData = additionalData;
        }

        public CoreResponse(bool success, dynamic error, dynamic errorMessage, dynamic additionalData, T data)
        {
            Success = success;
            Error = error;
            ErrorMessage = errorMessage;
            AdditionalData = additionalData;
            Data = data;
        }

        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("error")]
        public dynamic Error { get; set; }

        [JsonPropertyName("errorMessage")]
        public dynamic ErrorMessage { get; set; }

        [JsonPropertyName("additionalData")]
        public dynamic AdditionalData { get; set; }

        [JsonPropertyName("data")]
        public T Data { get; set; }
    }
}
