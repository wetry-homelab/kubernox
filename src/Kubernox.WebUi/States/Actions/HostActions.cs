using Kubernox.Shared;
using Kubernox.WebUi.Core;

namespace Kubernox.WebUi.States.Actions
{
    public class FetchHostAction
    {
    }

    public class FetchHostActionSuccess
    {
        public FetchHostActionSuccess(ICollection<HostItemResponse> hosts)
        {
            Hosts = hosts;
        }

        public ICollection<HostItemResponse> Hosts { get; }

    }

    public class FetchHostActionFailure : CoreFailureAction
    {
        public FetchHostActionFailure(string errorMessage, bool isError) : base(errorMessage, isError)
        {
        }
    }
}

