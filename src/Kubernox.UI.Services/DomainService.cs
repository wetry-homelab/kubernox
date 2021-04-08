using Infrastructure.Contracts.Request;
using Infrastructure.Contracts.Response;
using Kubernox.UI.Services.Interfaces;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kubernox.UI.Services
{
    public class DomainService : IDomainService
    {
        private readonly HttpClient httpClient;

        public DomainService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<bool> CreateDomainNameAsync(DomainNameCreateRequest request)
        {
            var httpResponse = await httpClient.PostAsync("api/domain", GetContent(request));
            return httpResponse.IsSuccessStatusCode;
        }

        public async Task<DomainItemResponse[]> GetDomainsAsync()
        {
            var httpResponse = await httpClient.GetAsync("api/domain");
            var response = await httpResponse.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<DomainItemResponse[]>(response);
        }

        public async Task<ClusterDomainItemResponse[]> GetDomainsForClusterAsync(string clusterId)
        {
            var httpResponse = await httpClient.GetAsync($"api/domain/cluster/{clusterId}");
            var response = await httpResponse.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ClusterDomainItemResponse[]>(response);
        }

        public async Task<bool> LinkDomainToClusterAsync(DomainLinkingRequestContract request)
        {
            var httpResponse = await httpClient.PostAsync($"api/domain/link-to-cluster", GetContent(request));
            return httpResponse.IsSuccessStatusCode;
        }

        public async Task<bool> ValidateDomainNameAsync(string id)
        {
            var httpResponse = await httpClient.GetAsync($"api/domain/{id}/validate");
            return httpResponse.IsSuccessStatusCode;
        }

        public HttpContent GetContent(object data)
        {
            return new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        }
    }
}
