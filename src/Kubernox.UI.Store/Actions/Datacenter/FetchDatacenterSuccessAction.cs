using Infrastructure.Contracts.Response;
using System.Collections.Generic;

namespace Kubernox.UI.Store.Actions.Datacenter
{
    public class FetchDatacenterSuccessAction
    {
        public FetchDatacenterSuccessAction(IEnumerable<DatacenterNodeResponse> nodes) =>
              Nodes = nodes;

        public IEnumerable<DatacenterNodeResponse> Nodes { get; }
    }
}
