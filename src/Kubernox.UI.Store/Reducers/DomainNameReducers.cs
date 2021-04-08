using Fluxor;
using Kubernox.UI.Store.Actions.DomainName;
using Kubernox.UI.Store.States;

namespace Kubernox.UI.Store.Reducers
{
    public static class DomainNameReducers
    {
        [ReducerMethod]
        public static DomainState ReduceFetchDomainAction(DomainState state, FetchDomainNameAction _) =>
            new DomainState(null, state.ClusterDomains, null, true, null);

        [ReducerMethod]
        public static DomainState ReduceFetchDomainSuccessAction(DomainState state, FetchDomainNameSuccessAction action) =>
            new DomainState(action.DomainNames, state.ClusterDomains, null, false, null);

        [ReducerMethod]
        public static DomainState ReduceFetchDomainFailureAction(DomainState state, FetchDomainNameFailureAction action) =>
            new DomainState(null, state.ClusterDomains, null, false, action.ErrorMessage);

        [ReducerMethod]
        public static DomainState ReduceFetchClusterDomainAction(DomainState state, FetchClusterDomainNameAction _) =>
            new DomainState(state.Domains, null, null, true, null);

        [ReducerMethod]
        public static DomainState ReduceFetchClusterDomainSuccessAction(DomainState state, FetchClusterDomainNameSuccessAction action) =>
            new DomainState(state.Domains, action.DomainNames, null, false, null);

        [ReducerMethod]
        public static DomainState ReduceFetchClusterDomainFailureAction(DomainState state, FetchClusterDomainNameFailureAction action) =>
            new DomainState(state.Domains, null, null, false, action.ErrorMessage);
    }
}
