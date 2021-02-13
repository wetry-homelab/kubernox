using Kubernox.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Kubernox
{
    class Program
    {
        private static readonly OrchestratorService orchestratorService = new OrchestratorService();

        static Task Main(string[] args)
        {
            var cancellationToken = new CancellationToken();
            return orchestratorService.StartDeploymentAsync(cancellationToken);
        }
    }
}
