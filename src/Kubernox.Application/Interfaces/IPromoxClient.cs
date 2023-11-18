using Corsinvest.ProxmoxVE.Api.Shared.Models.Cluster;

namespace Kubernox.Application.Interfaces
{
    public interface IProxmoxClient
    {
        Task<IEnumerable<ClusterResource>> GetNodesAsync(string ip, string apiToken);
    }
}
