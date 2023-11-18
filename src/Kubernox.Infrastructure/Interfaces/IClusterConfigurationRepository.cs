using Kubernox.Domain.Entities;

namespace Kubernox.Infrastructure.Interfaces
{
    public interface IClusterConfigurationRepository
    {
        Task<List<ClusterConfiguration>> GetClustersAsync();
    }
}
