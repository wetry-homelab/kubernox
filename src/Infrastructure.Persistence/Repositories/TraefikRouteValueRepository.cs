using Application.Entities;
using Application.Interfaces;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class TraefikRouteValueRepository : ITraefikRouteValueRepository
    {
        private readonly ILogger<TraefikRouteValueRepository> logger;
        private readonly ServiceDbContext serviceDbContext;

        public TraefikRouteValueRepository(ILogger<TraefikRouteValueRepository> logger, ServiceDbContext clusterDbContext)
        {
            this.logger = logger;
            this.serviceDbContext = clusterDbContext;
        }

        public Task<int> DeleteAsync(TraefikRouteValue entity)
        {
            serviceDbContext.Remove(entity);
            return serviceDbContext.SaveChangesAsync();
        }

        public Task<int> DeletesAsync(TraefikRouteValue[] entities)
        {
            serviceDbContext.RemoveRange(entities);
            return serviceDbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(TraefikRouteValue entity)
        {
            serviceDbContext.Add(entity);
            return serviceDbContext.SaveChangesAsync();
        }

        public Task<int> InsertsAsync(TraefikRouteValue[] entities)
        {
            serviceDbContext.AddRange(entities);
            return serviceDbContext.SaveChangesAsync();
        }

        public Task<TraefikRouteValue> ReadAsync(Expression<Func<TraefikRouteValue, bool>> predicate)
        {
            return serviceDbContext.TraefikRouteValue.FirstOrDefaultAsync(predicate);
        }

        public Task<TraefikRouteValue[]> ReadsAsync()
        {
            return serviceDbContext.TraefikRouteValue.ToArrayAsync();
        }

        public Task<TraefikRouteValue[]> ReadsAsync(Expression<Func<TraefikRouteValue, bool>> predicate)
        {
            return serviceDbContext.TraefikRouteValue.Where(predicate).ToArrayAsync();
        }

        public Task<int> UpdateAsync(TraefikRouteValue entity)
        {
            serviceDbContext.Entry(entity).State = EntityState.Modified;
            return serviceDbContext.SaveChangesAsync();
        }

        public async Task<int> UpdatesAsync(TraefikRouteValue[] entities)
        {
            var result = 0;
            foreach (var entity in entities)
                result = await UpdateAsync(entity);
            return result;
        }
    }
}
