using Infrastructure.Contracts.Request;
using Infrastructure.Contracts.Response;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDomainBusiness
    {
        Task<DomainItemResponse[]> ListDomainsAsync();
        Task<ClusterDomainItemResponse[]> ListDomainsForClusterAsync(string clusterId);
        Task<bool> CreateDomainAsync(DomainNameCreateRequest request);
        Task<bool> ValidateDomainAsync(string id);
        Task<bool> DeleteDomainAsync(string id);
        Task<bool> LinkDomainToClusterAsync(DomainLinkingRequestContract request);
    }
}
