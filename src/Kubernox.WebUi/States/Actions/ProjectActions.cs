using Kubernox.Shared.Contracts.Request;
using Kubernox.WebUi.Core;

namespace Kubernox.WebUi.States.Actions
{
    public class FetchProjectAction
    {
    }

    public class FetchProjectActionSuccess
    {
        public FetchProjectActionSuccess(IEnumerable<ProjectItemResponse> projects)
        {
            Projects = projects;
        }

        public IEnumerable<ProjectItemResponse> Projects { get; }
    }

    public class FetchProjectActionFailure : CoreFailureAction
    {
        public FetchProjectActionFailure(string errorMessage, bool isError) : base(errorMessage, isError)
        {
        }
    }
}
