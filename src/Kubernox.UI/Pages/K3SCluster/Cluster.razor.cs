using Kubernox.UI.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Kubernox.UI.Pages.K3SCluster
{
    public partial class Cluster : Fluxor.Blazor.Web.Components.FluxorComponent
    {
        [Parameter]
        public string ClusterId { get; set; }

        [Inject]
        IClusterService ClusterService { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        protected async Task DeleteClusterAsync()
        {
            if (await ClusterService.DeleteClustersAsync(ClusterId))
            {
                NavigationManager.NavigateTo("/k3s");
            }
        }
    }
}
