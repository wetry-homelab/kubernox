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
    public class SshKeyService : ISshKeyService
    {
        private readonly HttpClient httpClient;

        public SshKeyService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<SshKeyResponse[]> GetSshKeysAsync()
        {
            var httpResponse = await httpClient.GetAsync("api/sshkey");
            var response = await httpResponse.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<SshKeyResponse[]>(response);
        }

        public async Task<SshKeyDownloadResponse> DownloadKeyAsync(int id, string type)
        {
            var httpResponse = await httpClient.GetAsync($"api/sshkey/{id}/download/{type}");
            var response = await httpResponse.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<SshKeyDownloadResponse>(response);
        }

        public async Task<bool> DeleteSshKeysAsync(int id)
        {
            var httpResponse = await httpClient.DeleteAsync($"api/sshkey/{id}");
            return httpResponse.IsSuccessStatusCode;
        }


        public async Task<bool> ImportSshKeysAsync(SshKeyCreateRequest request)
        {
            var httpResponse = await httpClient.PostAsync("api/sshkey", GetContent(request));
            return httpResponse.IsSuccessStatusCode;
        }

        public HttpContent GetContent(object data)
        {
            return new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        }
    }
}
