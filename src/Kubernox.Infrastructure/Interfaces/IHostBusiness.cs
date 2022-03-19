using Kubernox.Infrastructure.Contracts.Request;
using Kubernox.Infrastructure.Contracts.Response;
using System;
using System.Threading.Tasks;

namespace Kubernox.Infrastructure.Interfaces
{
    public interface IHostBusiness
    {
        Task<HostListItemResponse[]> GetHostListAsync();
        Task<Guid?> CreateHostAsync(HostCreateRequest request);
    }
}
