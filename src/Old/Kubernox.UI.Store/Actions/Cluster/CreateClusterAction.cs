using Infrastructure.Contracts.Request;

namespace Kubernox.UI.Store.Actions.Cluster
{
    public class CreateClusterAction
    {
        public CreateClusterAction(ClusterCreateRequest request)
        {
            Request = request;
        }

        public ClusterCreateRequest Request { get; }
    }
}
