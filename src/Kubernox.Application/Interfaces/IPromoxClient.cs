using Corsinvest.ProxmoxVE.Api.Shared.Models.Cluster;

namespace Kubernox.Application.Interfaces
{
    public interface IProxmoxClient
    {
        Task<bool> AuthenticateUserAsync(string username, string password);
        Task<IEnumerable<ClusterResource>> GetNodesAsync(string ip, string apiToken);
    }
}
