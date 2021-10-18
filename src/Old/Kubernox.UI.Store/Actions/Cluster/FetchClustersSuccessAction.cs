using Infrastructure.Contracts.Response;
using System.Collections.Generic;

namespace Kubernox.UI.Store.Actions.Cluster
{
    public class FetchClustersSuccessAction
    {
        public FetchClustersSuccessAction(IEnumerable<ClusterItemResponse> clusters) =>
                Clusters = clusters;

        public IEnumerable<ClusterItemResponse> Clusters { get; }
    }
}
