using AntDesign;
using Fluxor;
using Infrastructure.Contracts.Request;
using Kubernox.UI.Services.Interfaces;
using Kubernox.UI.Store.Actions.Cluster;
using Kubernox.UI.Store.Actions.Datacenter;
using Kubernox.UI.Store.Actions.SshKey;
using Kubernox.UI.Store.Actions.Template;
using Kubernox.UI.Store.States;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Kubernox.UI.Pages.K3SCluster
{
    public partial class Index : Fluxor.Blazor.Web.Components.FluxorComponent
    {
        [Inject]
        private IState<ClusterState> ClusterState { get; set; }

        [Inject]
        private IState<DatacenterState> DatacenterState { get; set; }

        [Inject]
        private IState<TemplateState> TemplateState { get; set; }

        [Inject]
        private IState<SshKeyState> SshKeyState { get; set; }

        [Inject]
        IDispatcher Dispatcher { get; set; }

        public bool Visible { get; set; }

        public bool IsLoad
        {
            get
            {
                return ClusterState != null && ClusterState.Value.IsLoading
                        && TemplateState != null && TemplateState.Value.IsLoading
                        && SshKeyState != null && SshKeyState.Value.IsLoading;
            }
        }

        protected override void OnInitialized()
        {
            ClusterState.StateChanged += ClusterState_StateChanged;
            SshKeyState.StateChanged += SshKeyState_StateChanged;
            TemplateState.StateChanged += TemplateState_StateChanged;
            DatacenterState.StateChanged += DatacenterState_StateChanged;
            base.OnInitialized();
        }

        private void DatacenterState_StateChanged(object sender, DatacenterState e)
        {
            StateHasChanged();
        }

        private void ClusterState_StateChanged(object sender, ClusterState e)
        {
            StateHasChanged();
        }

        private void TemplateState_StateChanged(object sender, TemplateState e)
        {
            StateHasChanged();
        }

        private void SshKeyState_StateChanged(object sender, SshKeyState e)
        {
            StateHasChanged();
        }
    }
}
