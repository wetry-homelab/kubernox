using Application.Interfaces;
using Infrastructure.Persistence.Contexts;
using Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class ClusterRepository : IClusterRepository
    {
        private readonly ILogger<ClusterRepository> logger;
        private readonly ServiceDbContext serviceDbContext;

        public ClusterRepository(ILogger<ClusterRepository> logger, ServiceDbContext clusterDbContext)
        {
            this.logger = logger;
            this.serviceDbContext = clusterDbContext;
        }

        public Task<Cluster> ReadAsync(Expression<Func<Cluster, bool>> predicate)
        {
            return serviceDbContext.Cluster.Include(i => i.Nodes).FirstOrDefaultAsync(predicate);
        }

        public Task<Cluster[]> ReadsAsync(Expression<Func<Cluster, bool>> predicate)
        {
            return serviceDbContext.Cluster.Include(i => i.Nodes).Where(predicate).ToArrayAsync();
        }

        public Task<int> InsertAsync(Cluster entity)
        {
            serviceDbContext.Cluster.Add(entity);
            return serviceDbContext.SaveChangesAsync();
        }

        public Task<int> UpdateAsync(Cluster entity)
        {
            serviceDbContext.Entry(entity).State = EntityState.Modified;
            return serviceDbContext.SaveChangesAsync();
        }

        public Task<int> GetMaxOrder()
        {
            return serviceDbContext.Cluster.MaxAsync(c => c.OrderId);
        }

        public Task<Cluster[]> ReadsAsync()
        {
            return serviceDbContext.Cluster.ToArrayAsync();
        }

        public Task<int> InsertsAsync(Cluster[] entities)
        {
            serviceDbContext.Cluster.AddRange(entities);
            return serviceDbContext.SaveChangesAsync();
        }

        public async Task<int> UpdatesAsync(Cluster[] entities)
        {
            var result = 0;
            foreach (var entity in entities)
                result = await UpdateAsync(entity);
            return result;
        }

        public Task<int> DeleteAsync(Cluster entity)
        {
            return UpdateAsync(entity);
        }

        public Task<int> DeletesAsync(Cluster[] entities)
        {
            return UpdatesAsync(entities);
        }
    }
}
