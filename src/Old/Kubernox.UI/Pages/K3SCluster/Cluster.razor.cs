using AntDesign;
using Fluxor;
using Infrastructure.Contracts.Response;
using Kubernox.UI.Core;
using Kubernox.UI.Services.Interfaces;
using Kubernox.UI.Store.Actions.Cluster;
using Kubernox.UI.Store.Actions.DomainName;
using Kubernox.UI.Store.States;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
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
        private IState<DomainState> DomainState { get; set; }

        [Inject]
        private IState<ClusterState> ClusterState { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        protected ITable table;

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                ClusterState.StateChanged += ClusterState_StateChanged;
                Dispatcher.Dispatch(new FetchClusterAction(ClusterId));
                
                DomainState.StateChanged += DomainNameState_StateChanged;
                Dispatcher.Dispatch(new FetchDomainNameAction());
                Dispatcher.Dispatch(new FetchClusterDomainNameAction(ClusterId));

            }

            base.OnAfterRender(firstRender);
        }

        private void DomainNameState_StateChanged(object sender, DomainState e)
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
