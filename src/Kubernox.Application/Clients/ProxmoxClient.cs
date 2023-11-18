using Corsinvest.ProxmoxVE.Api;
using Corsinvest.ProxmoxVE.Api.Shared.Models.Cluster;

using Kubernox.Application.Interfaces;

using Newtonsoft.Json;

namespace Kubernox.Application.Clients
{
    public class ProxmoxClient : IProxmoxClient
    {
        public async Task<IEnumerable<ClusterResource>> GetNodesAsync(string ip, string apiToken)
        {
            var client = new PveClient(ip);
            client.ApiToken = apiToken;

            var nodesResult = await client.Cluster.Resources.Resources(type: "node");
            return JsonConvert.DeserializeObject<List<ClusterResource>>(JsonConvert.SerializeObject(nodesResult.ToEnumerable()));
        }
    }
}
