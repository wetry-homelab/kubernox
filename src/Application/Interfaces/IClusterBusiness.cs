using Infrastructure.Contracts.Request;
using Infrastructure.Contracts.Response;
using System;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IClusterBusiness
    {
        Task<ClusterItemResponse[]> ListClusterAsync();
        Task<ClusterDetailsResponse> GetClusterAsync(string id);
        Task<bool> CreateClusterAsync(ClusterCreateRequest request);
        Task<(bool found, bool update)> UpdateClusterAsync(string id, ClusterUpdateRequest request);
        Task<(bool found, bool update)> DeleteClusterAsync(string id);
        Task<(bool found, bool restart)> RestartClusterMasterAsync(string id);
        Task<(bool found, bool ready, KubeconfigDownloadResponse file)> DownloadKubeconfigAsync(string id);
        Task<bool> RefreshClusterRulesAsync(string id);
    }
}
