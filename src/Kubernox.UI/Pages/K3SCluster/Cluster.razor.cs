using Fluxor;
using Kubernox.UI.Services.Interfaces;
using Kubernox.UI.Store.Actions.Cluster;
using Kubernox.UI.Store.States;
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
        IDispatcher Dispatcher { get; set; }

        [Inject]
        private IState<ClusterState> ClusterState { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                Dispatcher.Dispatch(new FetchClusterAction(ClusterId));
                ClusterState.StateChanged += ClusterState_StateChanged;
            }

            base.OnAfterRender(firstRender);
        }

        private void ClusterState_StateChanged(object sender, ClusterState e)
        {
            StateHasChanged();
        }

        protected async Task DeleteClusterAsync()
        {
            if (await ClusterService.DeleteClustersAsync(ClusterId))
            {
                NavigationManager.NavigateTo("/k3s");
            }
        }
    }
}
