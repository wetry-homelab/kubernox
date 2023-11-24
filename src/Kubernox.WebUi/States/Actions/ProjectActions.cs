using Kubernox.Shared;
using Kubernox.WebUi.Core;

namespace Kubernox.WebUi.States.Actions
{
    public class FetchProjectAction
    {
    }

    public class FetchProjectActionSuccess
    {
        public FetchProjectActionSuccess(ICollection<ProjectItemResponse> projects)
        {
            Projects = projects;
        }

        public ICollection<ProjectItemResponse> Projects { get; }
    }

    public class FetchProjectActionFailure : CoreFailureAction
    {
        public FetchProjectActionFailure(string errorMessage, bool isError) : base(errorMessage, isError)
        {
        }
    }
}
