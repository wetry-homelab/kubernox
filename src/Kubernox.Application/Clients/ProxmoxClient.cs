using Corsinvest.ProxmoxVE.Api;
using Corsinvest.ProxmoxVE.Api.Shared.Models.Cluster;

using Kubernox.Application.Interfaces;

using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;

namespace Kubernox.Application.Clients
{
    public class ProxmoxClient : IProxmoxClient
    {
        private readonly IConfiguration configuration;
        private readonly string identityIp;

        public ProxmoxClient(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.identityIp = configuration["Identity:Ip"];
        }

        public Task<bool> AuthenticateUserAsync(string username, string password)
        {
            var client = new PveClient(identityIp);
            return client.Login(username, password);
        }

        public async Task<IEnumerable<ClusterResource>> GetNodesAsync(string ip, string apiToken)
        {
            var client = new PveClient(ip);
            client.ApiToken = apiToken;

            var nodesResult = await client.Cluster.Resources.Resources(type: "node");
            return JsonConvert.DeserializeObject<List<ClusterResource>>(JsonConvert.SerializeObject(nodesResult.ToEnumerable()));
        }

        public async Task<IEnumerable<ClusterResource>> GetVmsAsync(string ip, string apiToken)
        {
            var client = new PveClient(ip);
            client.ApiToken = apiToken;

            var nodesResult = (await client.Cluster.Resources.Resources(type: "vm")).ToEnumerable();
            return JsonConvert.DeserializeObject<List<ClusterResource>>(JsonConvert.SerializeObject(nodesResult));
        }
    }
}
