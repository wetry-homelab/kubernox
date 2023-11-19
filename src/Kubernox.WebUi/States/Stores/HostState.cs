using Fluxor;

using Kubernox.Shared.Contracts.Response;

namespace Kubernox.WebUi.States.Stores
{
    [FeatureState]
    public class HostState
    {
        public IEnumerable<HostItemResponse> Hosts { get; }

        public bool IsLoading { get; }

        public HostState()
        {

        }

        public HostState(IEnumerable<HostItemResponse> hosts, bool isLoading)
        {
            Hosts = hosts;
            IsLoading = isLoading;
        }
    }
}
