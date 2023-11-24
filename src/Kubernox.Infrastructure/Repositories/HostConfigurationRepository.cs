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

        public async Task<int> AddHostConfigurationAsync(HostConfiguration entity)
        {
            await kubernoxDbContext.AddAsync(entity);
            return await kubernoxDbContext.SaveChangesAsync();
        }

        public Task<List<HostConfiguration>> GetHostsAsync()
        {
            return kubernoxDbContext.HostConfigurations.Where(c => c.DeleteAt == null).ToListAsync();
        }

        public async Task<int> UpdateHostConfigurationAsync(HostConfiguration entity)
        {
            kubernoxDbContext.Entry(entity).State = EntityState.Modified;
            return await kubernoxDbContext.SaveChangesAsync();
        }
    }
}
