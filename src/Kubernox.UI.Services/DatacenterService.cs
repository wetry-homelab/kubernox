using Infrastructure.Contracts.Response;
using Kubernox.UI.Services.Interfaces;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services
{
    public class DatacenterService : IDatacenterService
    {
        private readonly HttpClient httpClient;

        public DatacenterService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<DatacenterNodeResponse[]> GetDatacentersAsync()
        {

            var httpResponse = await httpClient.GetAsync("api/datacenter");
            var response = await httpResponse.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<DatacenterNodeResponse[]>(response);
        }
    }
}
