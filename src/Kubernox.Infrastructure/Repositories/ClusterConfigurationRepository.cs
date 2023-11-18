using Kubernox.Domain.Entities;
using Kubernox.Infrastructure.Database;
using Kubernox.Infrastructure.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Kubernox.Infrastructure.Repositories
{
    public class ClusterConfigurationRepository : IClusterConfigurationRepository
    {
        private readonly KubernoxDbContext kubernoxDbContext;

        public ClusterConfigurationRepository(KubernoxDbContext kubernoxDbContext)
        {
            this.kubernoxDbContext = kubernoxDbContext;
        }

        public Task<List<ClusterConfiguration>> GetClustersAsync()
        {
            return kubernoxDbContext.ClusterConfigurations.ToListAsync();
        }
    }
}
