using Fluxor;

using Kubernox.Shared;

namespace Kubernox.WebUi.States.Stores
{
    [FeatureState]
    public class HostState
    {
        public ICollection<HostItemResponse> Hosts { get; }

        public bool IsLoading { get; }

        public HostState()
        {

        }

        public HostState(ICollection<HostItemResponse> hosts, bool isLoading)
        {
            Hosts = hosts;
            IsLoading = isLoading;
        }
    }
}
