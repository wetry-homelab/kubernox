using Infrastructure.Contracts.Request;
using Infrastructure.Contracts.Response;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDomaineNameBusiness
    {
        Task<DomainNameItemResponse[]> ListDomainsAsync();
        Task<bool> CreateDomainAsync(DomainNameCreateRequest request);
        Task<bool> ValidateDomainAsync(string id);
    }
}
