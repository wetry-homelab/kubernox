using Infrastructure.Contracts.Request;
using Infrastructure.Contracts.Response;
using System.Threading.Tasks;

namespace Kubernox.UI.Services.Interfaces
{
    public interface IDomainNameService
    {
        Task<DomainNameItemResponse[]> GetDomainsAsync();
        Task<bool> CreateDomainNameAsync(DomainNameCreateRequest request);
        Task<bool> ValidateDomainNameAsync(string id);
    }
}
