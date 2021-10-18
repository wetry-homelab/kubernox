using Fluxor;
using Infrastructure.Contracts.Request;
using Kubernox.UI.Store.Actions.Cluster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kubernox.UI.Components.Modals
{
    public partial class ClusterCreateModal : Fluxor.Blazor.Web.Components.FluxorComponent
    {
        [Parameter]
        public bool Visible { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnCloseCallback { get; set; }

        [Inject]
        public IStringLocalizer<App> Translator { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; }

        public ClusterCreateRequest ClusterCreateRequest { get; set; } = new ClusterCreateRequest()
        {
            Memory = 1024,
            Cpu = 1,
            SelectedTemplate = -1,
            Storage = 20,
            Node = 2,
            DeployNodeId = -1
        };

        private async Task HandleOk(MouseEventArgs e)
        {
            Dispatcher.Dispatch(new CreateClusterAction(ClusterCreateRequest));
            await OnCloseCallback.InvokeAsync(e);
            Visible = false;
            StateHasChanged();
        }

        private void HandleCancel(MouseEventArgs e)
        {
            OnCloseCallback.InvokeAsync(e).Wait();
            StateHasChanged();
        }
    }
}
