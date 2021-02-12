using Fluxor;
using Kubernox.UI.Store.Actions.Datacenter;
using Kubernox.UI.Store.States;
using Microsoft.AspNetCore.Components;

namespace Kubernox.UI.Pages.Datacenter
{
    public partial class Node : Fluxor.Blazor.Web.Components.FluxorComponent
    {
        [Parameter]
        public int NodeId { get; set; }

        [Inject]
        protected IState<DatacenterState> DatacenterState { get; set; }

        [Inject]
        IDispatcher Dispatcher { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            Dispatcher.Dispatch(new SelectDatacenterNodeAction(NodeId));
            DatacenterState.StateChanged += DatacenterState_StateChanged;
            base.OnAfterRender(firstRender);
        }

        private void DatacenterState_StateChanged(object sender, DatacenterState e)
        {
            StateHasChanged();
        }
    }
}
