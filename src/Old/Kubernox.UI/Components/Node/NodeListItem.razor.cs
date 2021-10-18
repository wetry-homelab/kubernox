using Infrastructure.Contracts.Response;
using Microsoft.AspNetCore.Components;

namespace Kubernox.UI.Components.Node
{
    public partial class NodeListItem : ComponentBase
    {
        [Parameter]
        public DatacenterNodeResponse Node { get; set; }
    }
}
