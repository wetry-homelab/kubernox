using Infrastructure.Contracts.Response;
using Kubernox.UI.Services.Interfaces;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services
{
    public class TemplateService : ITemplateService
    {
        private readonly HttpClient httpClient;

        public TemplateService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<TemplateResponse[]> GetTemplatesAsync()
        {
            var httpResponse = await httpClient.GetAsync("api/template");
            var response = await httpResponse.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TemplateResponse[]>(response);
        }
    }
}
