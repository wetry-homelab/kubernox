namespace Kubernox.UI.Store.Actions.Datacenter
{
    public class SelectDatacenterNodeAction
    {
        public int NodeId { get; }

        public SelectDatacenterNodeAction(int nodeId)
        {
            NodeId = nodeId;
        }
    }
}
