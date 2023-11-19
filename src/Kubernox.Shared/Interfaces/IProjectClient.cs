using Kubernox.Shared.Contracts.Request;

namespace Kubernox.Shared.Interfaces
{
    public interface IProjectClient
    {
        Task<IEnumerable<ProjectItemResponse>> GetProjectsAsync();
        Task<bool> CreateProjectAsync(CreateProjectRequest request);
    }
}
