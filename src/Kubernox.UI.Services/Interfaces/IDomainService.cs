using Infrastructure.Contracts.Request;
using Infrastructure.Contracts.Response;
using System.Threading.Tasks;

namespace Kubernox.UI.Services.Interfaces
{
    public interface IDomainService
    {
        Task<DomainItemResponse[]> GetDomainsAsync();
        Task<ClusterDomainItemResponse[]> GetDomainsForClusterAsync(string clusterId);
        Task<bool> CreateDomainNameAsync(DomainNameCreateRequest request);
        Task<bool> ValidateDomainNameAsync(string id);
        Task<bool> LinkDomainToClusterAsync(DomainLinkingRequestContract request);
    }
}
