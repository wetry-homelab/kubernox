using Infrastructure.Contracts.Response;

namespace Kubernox.UI.Store.States
{
    public class DatacenterState : BaseState
    {
        public DatacenterNodeResponse[] Nodes { get; }

        public DatacenterNodeResponse SelectedNode { get; }

        public DatacenterState(DatacenterNodeResponse[] nodes, DatacenterNodeResponse selectedNode, bool isLoading, string error)
            : base(isLoading, error)
        {
            Nodes = nodes;
            SelectedNode = selectedNode;
        }
    }
}
