using System.Text.Json;

using Kubernox.Shared.Contracts.Response;
using Kubernox.Shared.Interfaces;

namespace Kubernox.Shared.Clients
{
    public class HostClient : IHostClient
    {
        private readonly HttpClient httpClient;

        public HostClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<HostItemResponse>> GetHostsAsync()
        {
            var httpClientResponse = await httpClient.GetAsync("host");
            return JsonSerializer.Deserialize<List<HostItemResponse>>(await httpClientResponse.Content.ReadAsStringAsync());
        }

    }
}
