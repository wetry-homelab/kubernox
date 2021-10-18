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
            new DatacenterState(null, state.SelectedNode, true, null);

        [ReducerMethod]
        public static DatacenterState ReduceFetchDatacenterSuccessAction(DatacenterState state, FetchDatacenterSuccessAction action) =>
            new DatacenterState(action.Nodes.ToArray(), state.SelectedNode, false, null);

        [ReducerMethod]
        public static DatacenterState ReduceFetchDatacenterFailureAction(DatacenterState state, FetchDatacenterFailureAction action) =>
            new DatacenterState(null, state.SelectedNode, false, action.ErrorMessage);

        [ReducerMethod]
        public static DatacenterState ReduceSelectDatacenterNodeAction(DatacenterState state, SelectDatacenterNodeAction action) =>
            new DatacenterState(state.Nodes, null, true, null);

        [ReducerMethod]
        public static DatacenterState ReduceSelectDatacenterNodeSuccessAction(DatacenterState state, SelectDatacenterNodeSuccessAction action) =>
            new DatacenterState(state.Nodes, action.SelectedNode, false, null);

        [ReducerMethod]
        public static DatacenterState ReduceSelectDatacenterNodeFailureAction(DatacenterState state, SelectDatacenterNodeFailureAction action) =>
            new DatacenterState(state.Nodes, null, false, action.ErrorMessage);
    }
}
