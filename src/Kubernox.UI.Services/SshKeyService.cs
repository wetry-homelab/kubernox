using Infrastructure.Contracts.Request;
using Infrastructure.Contracts.Response;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services
{
    public class SshKeyService
    {
        public async Task<SshKeyResponse[]> GetSshKeysAsync()
        {
            using (var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:5001")
            })
            {
                var httpResponse = await httpClient.GetAsync("api/sshkey");
                var response = await httpResponse.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<SshKeyResponse[]>(response);
            }
        }

        public async Task<SshKeyDownloadResponse> DownloadKeyAsync(int id, string type)
        {
            using (var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:5001")
            })
            {
                var httpResponse = await httpClient.GetAsync($"api/sshkey/{id}/download/{type}");
                var response = await httpResponse.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<SshKeyDownloadResponse>(response);
            }
        }

        public async Task<bool> DeleteSshKeysAsync(int id)
        {
            using (var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:5001")
            })
            {
                var httpResponse = await httpClient.DeleteAsync($"api/sshkey/{id}");
                return httpResponse.IsSuccessStatusCode;
            }
        }


        public async Task<bool> ImportSshKeysAsync(SshKeyCreateRequest request)
        {
            using (var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:5001")
            })
            {
                var httpResponse = await httpClient.PostAsync("api/sshkey", GetContent(request));
                return httpResponse.IsSuccessStatusCode;
            }
        }

        public HttpContent GetContent(object data)
        {
            return new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        }
    }
}
