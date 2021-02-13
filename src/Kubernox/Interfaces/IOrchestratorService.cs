using System.Threading;
using System.Threading.Tasks;

namespace Kubernox.Interfaces
{
    public interface IOrchestratorService
    {
        Task StartDeploymentAsync(CancellationToken cancellationToken);
    }
}
