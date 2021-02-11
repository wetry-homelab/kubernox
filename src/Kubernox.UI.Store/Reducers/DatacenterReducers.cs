using Fluxor;
using Kubernox.UI.Store.Actions.Datacenter;
using Kubernox.UI.Store.States;
using System.Linq;

namespace Kubernox.UI.Store.Reducers
{
    public static class DatacenterReducers
    {
        [ReducerMethod]
        public static DatacenterState ReduceFetchDatacenterAction(DatacenterState state, FetchDatacenterAction _) =>
            new DatacenterState(null, true, null);

        [ReducerMethod]
        public static DatacenterState ReduceFetchDatacenterSuccessAction(DatacenterState state, FetchDatacenterSuccessAction action) =>
            new DatacenterState(action.Nodes.ToArray(), false, null);

        [ReducerMethod]
        public static DatacenterState ReduceFetchDatacenterFailureAction(DatacenterState state, FetchDatacenterFailureAction action) =>
            new DatacenterState(null, false, action.ErrorMessage);
    }
}
