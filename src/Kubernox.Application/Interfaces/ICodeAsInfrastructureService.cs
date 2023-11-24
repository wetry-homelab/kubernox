using Kubernox.Application.Models;

namespace Kubernox.Application.Interfaces
{
    public interface ICodeAsInfrastructureService
    {
        Task<bool> Deploy(DeploymentModel deploymentModel);
    }
}
