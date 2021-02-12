using Infrastructure.Contracts.Request;
using Infrastructure.Contracts.Response;
using System.Threading.Tasks;

namespace Kubernox.UI.Services.Interfaces
{
    public interface IClusterService
    {
        Task<ClusterItemResponse[]> GetClustersAsync();
        Task<bool> CreateClustersAsync(ClusterCreateRequest request);
        Task<KubeconfigDownloadResponse> DownloadConfigAsync(string id);
    }
}
