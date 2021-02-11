using Fluxor;
using Kubernox.UI.Store.Actions.Cluster;
using Kubernox.UI.Store.States;
using System.Linq;

namespace Kubernox.UI.Store.Reducers
{
    public static class ClusterReducer
    {
        [ReducerMethod]
        public static ClusterState ReduceFetchClusterAction(ClusterState state, FetchClusterAction _) =>
           new ClusterState(null, true, null);

        [ReducerMethod]
        public static ClusterState ReduceFetchClusterSuccessAction(ClusterState state, FetchClusterSuccessAction action) =>
            new ClusterState(action.Clusters.ToArray(), false, null);

        [ReducerMethod]
        public static ClusterState ReduceFetchClusterFailureAction(ClusterState state, FetchClusterFailureAction action) =>
            new ClusterState(null, false, action.ErrorMessage);

        [ReducerMethod]
        public static ClusterState ReduceCreateClusterAction(ClusterState state, CreateClusterAction _) =>
           new ClusterState(state.Clusters, true, null);

        [ReducerMethod]
        public static ClusterState ReduceCreateClusterSuccessAction(ClusterState state, CreateClusterSuccessAction action) =>
            new ClusterState(state.Clusters, false, null);

        [ReducerMethod]
        public static ClusterState ReduceCreateClusterFailureAction(ClusterState state, CreateClusterFailureAction action) =>
            new ClusterState(state.Clusters, false, action.ErrorMessage);

    }
}
