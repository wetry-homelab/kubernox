using Kubernox.Infrastructure.Contracts.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kubernox.Infrastructure.Interfaces
{
    public interface ILogBusiness
    {
        Task<LogResponse> GetLogsAsync(int start, int max);
    }
}
