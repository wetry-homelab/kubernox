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
    public class DomainNameRepository : IDomainNameRepository
    {
        private readonly ILogger<DomainNameRepository> logger;
        private readonly ServiceDbContext serviceDbContext;

        public DomainNameRepository(ILogger<DomainNameRepository> logger, ServiceDbContext clusterDbContext)
        {
            this.logger = logger;
            this.serviceDbContext = clusterDbContext;
        }

        public Task<int> DeleteAsync(Domain entity)
        {
            return UpdateAsync(entity);
        }

        public Task<int> DeletesAsync(Domain[] entities)
        {
            return UpdatesAsync(entities);
        }

        public Task<int> InsertAsync(Domain entity)
        {
            serviceDbContext.Domain.Add(entity);
            return serviceDbContext.SaveChangesAsync();
        }

        public Task<int> InsertsAsync(Domain[] entities)
        {
            serviceDbContext.Domain.AddRange(entities);
            return serviceDbContext.SaveChangesAsync();
        }

        public Task<Domain> ReadAsync(Expression<Func<Domain, bool>> predicate)
        {
            return serviceDbContext.Domain.FirstOrDefaultAsync(predicate);
        }

        public Task<Domain[]> ReadsAsync()
        {
            return serviceDbContext.Domain.ToArrayAsync();
        }

        public Task<Domain[]> ReadsAsync(Expression<Func<Domain, bool>> predicate)
        {
            return serviceDbContext.Domain.Where(predicate).ToArrayAsync();
        }

        public Task<int> UpdateAsync(Domain entity)
        {
            serviceDbContext.Entry(entity).State = EntityState.Modified;
            return serviceDbContext.SaveChangesAsync();
        }

        public async Task<int> UpdatesAsync(Domain[] entities)
        {
            var result = 0;
            foreach (var entity in entities)
                result = await UpdateAsync(entity);
            return result;
        }
    }
}
