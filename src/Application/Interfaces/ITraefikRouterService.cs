using Application.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITraefikRouterService
    {
        Task LoadAndRefreshRouting(CancellationToken stoppingToken);
        Task GenerateClusterBasicRules(Cluster cluster);
        Task StoreNewRule(Cluster cluster, string domain);
        Task StoreNewHttpRule(Cluster cluster, string domain);
        Task StoreNewHttpsRule(Cluster cluster, string subDomain, string domain);
        Task RefreshClusterRule(string clusterId);
        Task DeleteClusterRules(Cluster cluster);
        Task DeleteRule(string ruleId);
        Task DeleteRule(string clusterName, string domain);
    }
}
