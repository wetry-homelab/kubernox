using Kubernox.Domain.Entities;

namespace Kubernox.Infrastructure.Interfaces
{
    public interface ILogRepository
    {
        Task<int> AddLogAsync(Log entity);
        Task<int> AddLogsAsync(List<Log> entities);
    }
}
