using Fluxor;

using Kubernox.Shared.Contracts.Request;

namespace Kubernox.WebUi.States.Stores
{
    [FeatureState]
    public class ProjectState
    {
        public IEnumerable<ProjectItemResponse> Projects { get; }

        public bool IsLoading { get; }

        public ProjectState()
        {

        }

        public ProjectState(IEnumerable<ProjectItemResponse> projects, bool isLoading)
        {
            Projects = projects;
            IsLoading = isLoading;
        }
    }
}
