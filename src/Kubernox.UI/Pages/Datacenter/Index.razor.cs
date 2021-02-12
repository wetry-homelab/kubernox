using AntDesign;
using Fluxor;
using Kubernox.UI.Store.Actions.Datacenter;
using Kubernox.UI.Store.States;
using Microsoft.AspNetCore.Components;

namespace Kubernox.UI.Pages.Datacenter
{
    public partial class Index : Fluxor.Blazor.Web.Components.FluxorComponent
    {
        [Inject]
        private IState<DatacenterState> DatacenterState { get; set; }

        [Inject]
        IDispatcher Dispatcher { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }


        protected ITable table;

        protected override void OnInitialized()
        {
            Dispatcher.Dispatch(new FetchDatacenterAction());
            DatacenterState.StateChanged += DatacenterState_StateChanged;
            base.OnInitialized();
        }

        private void DatacenterState_StateChanged(object sender, DatacenterState e)
        {
            StateHasChanged();
        }

        protected void ViewDetails(int id)
        {
            NavigationManager.NavigateTo($"/node/{id}");
        }
    }
}
