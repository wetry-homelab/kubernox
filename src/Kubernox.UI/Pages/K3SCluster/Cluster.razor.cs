using Fluxor;
using Kubernox.UI.Core;
using Kubernox.UI.Services.Interfaces;
using Kubernox.UI.Store.Actions.Cluster;
using Kubernox.UI.Store.Actions.DomainName;
using Kubernox.UI.Store.States;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Kubernox.UI.Pages.K3SCluster
{
    public partial class Cluster : CoreComponent
    {
        [Parameter]
        public string ClusterId { get; set; }

        [Inject]
        IClusterService ClusterService { get; set; }

        [Inject]
        private IState<DomainNameState> DomainNameState { get; set; }

        [Inject]
        private IState<ClusterState> ClusterState { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                ClusterState.StateChanged += ClusterState_StateChanged;
                Dispatcher.Dispatch(new FetchClusterAction(ClusterId));
                
                DomainNameState.StateChanged += DomainNameState_StateChanged;
                Dispatcher.Dispatch(new FetchDomainNameAction());
            }

            base.OnAfterRender(firstRender);
        }

        private void DomainNameState_StateChanged(object sender, DomainNameState e)
        {
            StateHasChanged();
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
