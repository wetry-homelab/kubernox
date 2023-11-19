using Kubernox.Shared.Contracts.Response;
using Kubernox.WebUi.Core;

namespace Kubernox.WebUi.States.Actions
{
    public class FetchHostAction
    {
    }

    public class FetchHostActionSuccess
    {
        public FetchHostActionSuccess(IEnumerable<HostItemResponse> hosts)
        {
            Hosts = hosts;
        }

        public IEnumerable<HostItemResponse> Hosts { get; }

    }

    public class FetchHostActionFailure : CoreFailureAction
    {
        public FetchHostActionFailure(string errorMessage, bool isError) : base(errorMessage, isError)
        {
        }
    }
}

