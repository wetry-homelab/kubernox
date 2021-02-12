using Infrastructure.Contracts.Request;
using Infrastructure.Contracts.Response;
using System.Threading.Tasks;

namespace Kubernox.UI.Services.Interfaces
{
    public interface ISshKeyService
    {
        Task<SshKeyResponse[]> GetSshKeysAsync();
        Task<SshKeyDownloadResponse> DownloadKeyAsync(int id, string type);
        Task<bool> DeleteSshKeysAsync(int id);
        Task<bool> ImportSshKeysAsync(SshKeyCreateRequest request);
    }
}
