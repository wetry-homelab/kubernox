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
    public class ClusterDomainRepository : IClusterDomainRepository
    {
        private readonly ILogger<ClusterDomainRepository> logger;
        private readonly ServiceDbContext serviceDbContext;

        public ClusterDomainRepository(ILogger<ClusterDomainRepository> logger, ServiceDbContext clusterDbContext)
        {
            this.logger = logger;
            this.serviceDbContext = clusterDbContext;
        }

        public Task<int> DeleteAsync(ClusterDomain entity)
        {
            serviceDbContext.ClusterDomain.Remove(entity);
            return serviceDbContext.SaveChangesAsync();
        }

        public Task<int> DeletesAsync(ClusterDomain[] entities)
        {
            serviceDbContext.ClusterDomain.RemoveRange(entities);
            return serviceDbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(ClusterDomain entity)
        {
            serviceDbContext.ClusterDomain.Add(entity);
            return serviceDbContext.SaveChangesAsync();
        }

        public Task<int> InsertsAsync(ClusterDomain[] entities)
        {
            throw new NotImplementedException();
        }

        public Task<ClusterDomain> ReadAsync(Expression<Func<ClusterDomain, bool>> predicate)
        {
            return serviceDbContext.ClusterDomain.FirstOrDefaultAsync(predicate);
        }

        public Task<ClusterDomain[]> ReadsAsync()
        {
            return serviceDbContext.ClusterDomain.ToArrayAsync();
        }

        public Task<ClusterDomain[]> ReadsAsync(Expression<Func<ClusterDomain, bool>> predicate)
        {
            return serviceDbContext.ClusterDomain.Where(predicate).ToArrayAsync();
        }

        public Task<int> UpdateAsync(ClusterDomain entity)
        {
            serviceDbContext.Entry(entity).State = EntityState.Modified;
            return serviceDbContext.SaveChangesAsync();
        }

        public Task<int> UpdatesAsync(ClusterDomain[] entities)
        {
            throw new NotImplementedException();
        }
    }
}
