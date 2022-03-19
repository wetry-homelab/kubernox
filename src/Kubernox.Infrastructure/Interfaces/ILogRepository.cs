using Kubernox.Infrastructure.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kubernox.Infrastructure.Interfaces
{
    public interface ILogRepository
    {
        Task<IEnumerable<Log>> ReadLogsAsync(int start = 0, int max = 100);
        Task<int> CountLogEntry();
    }
}
