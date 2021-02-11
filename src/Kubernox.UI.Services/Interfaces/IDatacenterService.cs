using Infrastructure.Contracts.Response;
using System.Threading.Tasks;

namespace Kubernox.UI.Services.Interfaces
{
    public interface IDatacenterService
    {
        Task<DatacenterNodeResponse[]> GetDatacentersAsync();
    }
}
