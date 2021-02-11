using Infrastructure.Contracts.Response;

namespace Kubernox.UI.Store.States
{
    public class DatacenterState : BaseState
    {
        public DatacenterNodeResponse[] Nodes { get; }

        public DatacenterState(DatacenterNodeResponse[] nodes, bool isLoading, string error)
            : base(isLoading, error)
        {
            Nodes = nodes;
        }
    }
}
