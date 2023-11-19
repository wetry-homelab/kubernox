using Fluxor;

using Kubernox.Shared.Interfaces;
using Kubernox.WebUi.States.Actions;
using Kubernox.WebUi.States.Stores;

using Microsoft.AspNetCore.Components;

namespace Kubernox.WebUi.Pages.Admin
{
    public partial class Hosts
    {
        [Inject]
        public IHostClient HostClient { get; set; }

        [Inject]
        private IState<HostState> HostState { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; }


        protected override async Task OnInitializedAsync()
        {
            Dispatcher.Dispatch(new FetchHostAction());
            await base.OnInitializedAsync();
        }
    }
}
