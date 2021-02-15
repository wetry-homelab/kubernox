using Infrastructure.Contracts.Request;
using Infrastructure.Contracts.Response;
using Kubernox.UI.Services.Interfaces;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services
{
    public class ClusterService : IClusterService
    {
        private readonly HttpClient httpClient;

        public ClusterService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ClusterItemResponse[]> GetClustersAsync()
        {
            var httpResponse = await httpClient.GetAsync("api/cluster");
            var response = await httpResponse.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ClusterItemResponse[]>(response);
        }

        public async Task<bool> DeleteClustersAsync(string id)
        {
            var httpResponse = await httpClient.DeleteAsync($"api/cluster/{id}");
            var response = await httpResponse.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<bool>(response);
        }

        public async Task<bool> CreateClustersAsync(ClusterCreateRequest request)
        {
            var httpResponse = await httpClient.PostAsync("api/cluster", GetContent(request));
            return httpResponse.IsSuccessStatusCode;
        }

        public async Task<KubeconfigDownloadResponse> DownloadConfigAsync(string id)
        {
            var httpResponse = await httpClient.GetAsync($"api/cluster/{id}/kubeconfig");
            var response = await httpResponse.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<KubeconfigDownloadResponse>(response);
        }

        public HttpContent GetContent(object data)
        {
            return new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        }
    }
}
