using System.Net.Http.Json;
using System.Text.Json;

using Kubernox.Shared.Contracts.Request;
using Kubernox.Shared.Contracts.Response;
using Kubernox.Shared.Interfaces;

namespace Kubernox.Shared.Clients
{
    public class IdentityClient : IIdentityClient
    {
        private readonly HttpClient httpClient;

        public IdentityClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<SignInResponse> SignInAsync(SignInRequest request)
        {
            var httpClientResponse = await httpClient.PostAsJsonAsync("identity", request);
            return JsonSerializer.Deserialize<SignInResponse>(await httpClientResponse.Content.ReadAsStringAsync());
        }
    }
}
