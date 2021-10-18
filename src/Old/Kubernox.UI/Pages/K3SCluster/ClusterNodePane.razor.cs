using Infrastructure.Contracts.Response;
using Microsoft.AspNetCore.Components;

namespace Kubernox.UI.Pages.K3SCluster
{
    public partial class ClusterNodePane : Fluxor.Blazor.Web.Components.FluxorComponent
    {
        [Parameter]
        public ClusterNodeDetailsResponse Node { get; set; }
    }
}
