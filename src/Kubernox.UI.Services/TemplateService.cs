using Infrastructure.Contracts.Response;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services
{
    public class TemplateService
    {
        public async Task<TemplateResponse[]> GetTemplatesAsync()
        {
            using (var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:5001")
            })
            {
                var httpResponse = await httpClient.GetAsync("api/template");
                var response = await httpResponse.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<TemplateResponse[]>(response);
            }
        }
    }
}
