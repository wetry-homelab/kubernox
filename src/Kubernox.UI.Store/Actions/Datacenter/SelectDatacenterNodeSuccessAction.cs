using Infrastructure.Contracts.Response;

namespace Kubernox.UI.Store.Actions.Datacenter
{
    public class SelectDatacenterNodeSuccessAction
    {
        public SelectDatacenterNodeSuccessAction(DatacenterNodeResponse selectedNode)
        {
            SelectedNode = selectedNode;
        }

        public DatacenterNodeResponse SelectedNode { get; }
    }
}
