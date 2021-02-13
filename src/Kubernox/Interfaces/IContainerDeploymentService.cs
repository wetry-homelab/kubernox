using Kubernox.Model;
using System.Threading;
using System.Threading.Tasks;

namespace Kubernox.Interfaces
{
    public interface IContainerDeploymentService
    {
        Task<bool> InstantiateNetworkAsync();
        Task<bool> InstantiateDatabaseContainer(PostgreDatabaseProvider database, CancellationToken cancellationToken);
        Task<bool> InstantiateQueueContainer(RabbitMqProvider queue, CancellationToken cancellationToken);
        Task<bool> InstantiateCacheContainer(RedisProvider redis, CancellationToken cancellationToken);
        Task<bool> InstantiatePrometheusContainer(PrometheusProvider prometheus, CancellationToken cancellationToken);
        Task<bool> InstantiateGrafanaContainer(PrometheusProvider prometheus, CancellationToken cancellationToken);
        Task<bool> InstantiateKubernoxServiceContainer(Configuration configuration, CancellationToken cancellationToken);
        Task<bool> InstantiateKubernoxWorkersContainer(Configuration configuration, CancellationToken cancellationToken);
        Task<bool> InstantiateKubernoxUiContainer(Configuration configuration, CancellationToken cancellationToken);
        Task<bool> InstantiateTraefikProxyContainer(Configuration configuration, CancellationToken cancellationToken);
    }
}
