using Kubernox.Domain.Entities;
using Kubernox.Infrastructure.Database;
using Kubernox.Infrastructure.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Kubernox.Infrastructure.Repositories
{
    public class HostConfigurationRepository : IHostConfigurationRepository
    {
        private readonly KubernoxDbContext kubernoxDbContext;

        public HostConfigurationRepository(KubernoxDbContext kubernoxDbContext)
        {
            this.kubernoxDbContext = kubernoxDbContext;
        }

        public Task<List<HostConfiguration>> GetHostsAsync()
        {
            return kubernoxDbContext.HostConfigurations.Where(c => c.DeleteAt == null).ToListAsync();
        }
    }
}
