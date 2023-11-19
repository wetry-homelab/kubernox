using Fluxor;

using Kubernox.WebUi.States.Actions;
using Kubernox.WebUi.States.Stores;

namespace Kubernox.WebUi.States.Reducers
{
    public static class HostReducers
    {
        [ReducerMethod]
        public static HostState ReduceFetchHostAction(HostState hostState, FetchHostAction _)
            => new HostState(hostState.Hosts, true);

        [ReducerMethod]
        public static HostState ReduceFetchHostActionSuccess(HostState _, FetchHostActionSuccess action)
            => new HostState(action.Hosts, false);

        [ReducerMethod]
        public static HostState ReduceFetchHostActionFailure(HostState hostState, FetchHostActionFailure _)
            => new HostState(hostState.Hosts, false);
    }
}
