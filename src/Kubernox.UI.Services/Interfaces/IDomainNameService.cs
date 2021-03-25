using Infrastructure.Contracts.Request;
using Infrastructure.Contracts.Response;
using System.Threading.Tasks;

namespace Kubernox.UI.Services.Interfaces
{
    public interface IDomainNameService
    {
        Task<DomainItemResponse[]> GetDomainsAsync();
        Task<ClusterDomainItemResponse[]> GetDomainsForClusterAsync(string clusterId);
        Task<bool> CreateDomainNameAsync(DomainNameCreateRequest request);
        Task<bool> ValidateDomainNameAsync(string id);
    }
}
