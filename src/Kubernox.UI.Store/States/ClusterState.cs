using Infrastructure.Contracts.Response;

namespace Kubernox.UI.Store.States
{
    public class ClusterState : BaseState
    {
        public ClusterItemResponse[] Clusters { get; }

        public ClusterDetailsResponse Cluster { get; }

        public ClusterState(ClusterItemResponse[] clusters, ClusterDetailsResponse cluster, bool isLoading, string error)
        : base(isLoading, error)
        {
            Clusters = clusters;
            Cluster = cluster;
        }
    }
}
