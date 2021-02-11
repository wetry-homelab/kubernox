using Fluxor;
using Microsoft.AspNetCore.Components;
using Kubernox.UI.Store.Actions.Datacenter;
using Kubernox.UI.Store.States;
using System.Threading.Tasks;

namespace Kubernox.UI.Pages.Datacenter
{
    public partial class Index : Fluxor.Blazor.Web.Components.FluxorComponent
    {
        [Inject]
        private IState<DatacenterState> DatacenterState { get; set; }

        [Inject]
        IDispatcher Dispatcher { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Dispatcher.Dispatch(new FetchDatacenterAction());
        }
    }
}
