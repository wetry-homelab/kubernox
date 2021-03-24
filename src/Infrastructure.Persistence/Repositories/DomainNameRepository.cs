using Application.Interfaces;
using Domain.Entities;
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

        public Task<int> DeleteAsync(DomainName entity)
        {
            return UpdateAsync(entity);
        }

        public Task<int> DeletesAsync(DomainName[] entities)
        {
            return UpdatesAsync(entities);
        }

        public Task<int> InsertAsync(DomainName entity)
        {
            serviceDbContext.DomainName.Add(entity);
            return serviceDbContext.SaveChangesAsync();
        }

        public Task<int> InsertsAsync(DomainName[] entities)
        {
            serviceDbContext.DomainName.AddRange(entities);
            return serviceDbContext.SaveChangesAsync();
        }

        public Task<DomainName> ReadAsync(Expression<Func<DomainName, bool>> predicate)
        {
            return serviceDbContext.DomainName.FirstOrDefaultAsync(predicate);
        }

        public Task<DomainName[]> ReadsAsync()
        {
            return serviceDbContext.DomainName.ToArrayAsync();
        }

        public Task<DomainName[]> ReadsAsync(Expression<Func<DomainName, bool>> predicate)
        {
            return serviceDbContext.DomainName.Where(predicate).ToArrayAsync();
        }

        public Task<int> UpdateAsync(DomainName entity)
        {
            serviceDbContext.Entry(entity).State = EntityState.Modified;
            return serviceDbContext.SaveChangesAsync();
        }

        public async Task<int> UpdatesAsync(DomainName[] entities)
        {
            var result = 0;
            foreach (var entity in entities)
                result = await UpdateAsync(entity);
            return result;
        }
    }
}
