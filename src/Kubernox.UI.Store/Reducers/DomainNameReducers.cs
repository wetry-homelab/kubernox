using Fluxor;
using Kubernox.UI.Store.Actions.DomainName;
using Kubernox.UI.Store.States;

namespace Kubernox.UI.Store.Reducers
{
    public static class DomainNameReducers
    {
        [ReducerMethod]
        public static DomainNameState ReduceFetchDomainNameAction(DomainNameState state, FetchDomainNameAction _) =>
            new DomainNameState(null, null, true, null);

        [ReducerMethod]
        public static DomainNameState ReduceFetchDomainNameSuccessAction(DomainNameState state, FetchDomainNameSuccessAction action) =>
            new DomainNameState(action.DomainNames, null, false, null);

        [ReducerMethod]
        public static DomainNameState ReduceFetchDomainNameFailureAction(DomainNameState state, FetchDomainNameFailureAction action) =>
            new DomainNameState(null, null, false, action.ErrorMessage);
    }
}
