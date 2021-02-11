using Infrastructure.Contracts.Response;

namespace Kubernox.UI.Store.States
{
    public class ClusterState : BaseState
    {
        public ClusterItemResponse[] Clusters { get; }

        public ClusterState(ClusterItemResponse[] clusters, bool isLoading, string error)
        : base(isLoading, error)
        {
            Clusters = clusters;
        }
    }
}
