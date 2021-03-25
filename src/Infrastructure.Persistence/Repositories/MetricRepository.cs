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
    public class MetricRepository : IMetricRepository
    {
        private readonly ILogger<MetricRepository> logger;
        private readonly ServiceDbContext serviceDbContext;

        public MetricRepository(ILogger<MetricRepository> logger, ServiceDbContext clusterDbContext)
        {
            this.logger = logger;
            this.serviceDbContext = clusterDbContext;
        }

        public Task<Metric[]> ReadsAsync(Expression<Func<Metric, bool>> predicate)
        {
            return serviceDbContext.Metric.Where(predicate).ToArrayAsync();
        }

        public Task<int> InsertsAsync(Metric[] metrics)
        {
            serviceDbContext.Metric.AddRange(metrics);
            return serviceDbContext.SaveChangesAsync();
        }

        public async Task InsertMetricsWithStrategyAsync(Metric[] metrics)
        {
            var strategy = serviceDbContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = serviceDbContext.Database.BeginTransaction())
                {
                    serviceDbContext.Metric.AddRange(metrics);
                    await serviceDbContext.SaveChangesAsync();
                    transaction.Commit();
                }
            });
        }

        public Task<Metric> ReadAsync(Expression<Func<Metric, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Metric[]> ReadsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(Metric entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Metric entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdatesAsync(Metric[] entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(Metric entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeletesAsync(Metric[] entities)
        {
            throw new NotImplementedException();
        }
    }
}
