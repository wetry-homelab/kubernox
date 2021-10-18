namespace Kubernox.UI.Store.Actions.Cluster
{
    public class FetchClusterAction
    {
        public string ClusterId { get; }

        public FetchClusterAction(string clusterId)
        {
            ClusterId = clusterId;
        }
    }
}
