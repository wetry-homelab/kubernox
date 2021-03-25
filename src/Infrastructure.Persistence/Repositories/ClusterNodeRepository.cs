using Application.Core;
using Application.Interfaces;
using Application.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class ClusterNodeRepository : IClusterNodeRepository
    {
        private readonly ILogger<ClusterNodeRepository> logger;
        private readonly ServiceDbContext serviceDbContext;

        public ClusterNodeRepository(ILogger<ClusterNodeRepository> logger, ServiceDbContext clusterDbContext)
        {
            this.logger = logger;
            this.serviceDbContext = clusterDbContext;
        }

        public Task<ClusterNode> ReadAsync(Expression<Func<ClusterNode, bool>> predicate)
        {
            return serviceDbContext.ClusterNode.FirstOrDefaultAsync(predicate);
        }

        public Task<ClusterNode[]> ReadsAsync(Expression<Func<ClusterNode, bool>> predicate)
        {
            return serviceDbContext.ClusterNode.Where(predicate).ToArrayAsync();
        }

        public Task<int> InsertAsync(ClusterNode entity)
        {
            serviceDbContext.ClusterNode.Add(entity);
            return serviceDbContext.SaveChangesAsync();
        }

        public Task<int> UpdateAsync(ClusterNode entity)
        {
            serviceDbContext.Entry(entity).State = EntityState.Modified;
            return serviceDbContext.SaveChangesAsync();
        }

        public Task<int> InsertsAsync(ClusterNode[] entity)
        {
            serviceDbContext.ClusterNode.AddRange(entity);
            return serviceDbContext.SaveChangesAsync();
        }

        public Task<ClusterNode[]> ReadsAsync()
        {
            return serviceDbContext.ClusterNode.ToArrayAsync();
        }

        public async Task<int> UpdatesAsync(ClusterNode[] entities)
        {
            var result = 0;
            foreach (var entity in entities)
                result = await UpdateAsync(entity);
            return result;
        }

        public Task<int> DeleteAsync(ClusterNode entity)
        {
            return UpdateAsync(entity);
        }

        public Task<int> DeletesAsync(ClusterNode[] entities)
        {
            return UpdatesAsync(entities);
        }
    }
}
