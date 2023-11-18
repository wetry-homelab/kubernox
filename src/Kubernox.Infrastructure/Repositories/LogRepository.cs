using Kubernox.Domain.Entities;
using Kubernox.Infrastructure.Database;
using Kubernox.Infrastructure.Interfaces;

namespace Kubernox.Infrastructure.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly KubernoxDbContext kubernoxDbContext;

        public LogRepository(KubernoxDbContext kubernoxDbContext)
        {
            this.kubernoxDbContext = kubernoxDbContext;
        }

        public async Task<int> AddLogAsync(Log entity)
        {
            await kubernoxDbContext.AddAsync(entity);
            return await kubernoxDbContext.SaveChangesAsync();
        }

        public async Task<int> AddLogsAsync(List<Log> entities)
        {
            await kubernoxDbContext.AddRangeAsync(entities);
            return await kubernoxDbContext.SaveChangesAsync();
        }
    }
}
