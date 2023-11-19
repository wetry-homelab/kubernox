using System.Net.Http.Json;
using System.Text.Json;

using Kubernox.Shared.Contracts.Request;
using Kubernox.Shared.Interfaces;

namespace Kubernox.Shared.Clients
{
    public class ProjectClient : IProjectClient
    {
        private readonly HttpClient httpClient;

        public ProjectClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<bool> CreateProjectAsync(CreateProjectRequest request)
        {
            var httpClientResponse = await httpClient.PostAsJsonAsync("project", request);
            return httpClientResponse.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<ProjectItemResponse>> GetProjectsAsync()
        {
            var httpClientResponse = await httpClient.GetAsync("project");
            return JsonSerializer.Deserialize<List<ProjectItemResponse>>(await httpClientResponse.Content.ReadAsStringAsync());
        }
    }
}
