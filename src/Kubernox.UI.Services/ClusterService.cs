using Infrastructure.Contracts.Request;
using Infrastructure.Contracts.Response;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services
{
    public class ClusterService
    {
        public async Task<ClusterItemResponse[]> GetClustersAsync()
        {
            using (var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:5001")
            })
            {
                var httpResponse = await httpClient.GetAsync("api/cluster");
                var response = await httpResponse.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<ClusterItemResponse[]>(response);
            }
        }

        public async Task<bool> CreateClustersAsync(ClusterCreateRequest request)
        {
            using (var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:5001")
            })
            {
                var httpResponse = await httpClient.PostAsync("api/cluster", GetContent(request));
                return httpResponse.IsSuccessStatusCode;
            }
        }

        public async Task<SshKeyDownloadResponse> DownloadConfigAsync(Guid id)
        {
            using (var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:5001")
            })
            {
                var httpResponse = await httpClient.GetAsync($"api/cluster/{id}/kubeconfig");
                var response = await httpResponse.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<SshKeyDownloadResponse>(response);
            }
        }

        public HttpContent GetContent(object data)
        {
            return new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        }
    }
}
