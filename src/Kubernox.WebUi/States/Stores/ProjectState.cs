using Fluxor;

using Kubernox.Shared;

namespace Kubernox.WebUi.States.Stores
{
    [FeatureState]
    public class ProjectState
    {
        public ICollection<ProjectItemResponse> Projects { get; }

        public bool IsLoading { get; }

        public ProjectState()
        {

        }

        public ProjectState(ICollection<ProjectItemResponse> projects, bool isLoading)
        {
            Projects = projects;
            IsLoading = isLoading;
        }
    }
}
