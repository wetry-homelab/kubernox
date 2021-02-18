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
            new ClusterState(state.Clusters, null, true, null);

        [ReducerMethod]
        public static ClusterState ReduceFetchClusterSuccessAction(ClusterState state, FetchClusterSuccessAction action) =>
            new ClusterState(state.Clusters, action.Cluster, false, null);

        [ReducerMethod]
        public static ClusterState ReduceFetchClusterFailureAction(ClusterState state, FetchClusterFailureAction action) =>
            new ClusterState(state.Clusters, null, false, action.ErrorMessage);

        [ReducerMethod]
        public static ClusterState ReduceFetchClustersAction(ClusterState state, FetchClustersAction _) =>
           new ClusterState(null, state.Cluster, true, null);

        [ReducerMethod]
        public static ClusterState ReduceFetchClustersSuccessAction(ClusterState state, FetchClustersSuccessAction action) =>
            new ClusterState(action.Clusters.ToArray(), state.Cluster, false, null);

        [ReducerMethod]
        public static ClusterState ReduceFetchClustersFailureAction(ClusterState state, FetchClustersFailureAction action) =>
            new ClusterState(null, state.Cluster, false, action.ErrorMessage);

        [ReducerMethod]
        public static ClusterState ReduceCreateClusterAction(ClusterState state, CreateClusterAction _) =>
           new ClusterState(state.Clusters, state.Cluster, true, null);

        [ReducerMethod]
        public static ClusterState ReduceCreateClusterSuccessAction(ClusterState state, CreateClusterSuccessAction action) =>
            new ClusterState(state.Clusters, state.Cluster, false, null);

        [ReducerMethod]
        public static ClusterState ReduceCreateClusterFailureAction(ClusterState state, CreateClusterFailureAction action) =>
            new ClusterState(state.Clusters, state.Cluster, false, action.ErrorMessage);

    }
}
