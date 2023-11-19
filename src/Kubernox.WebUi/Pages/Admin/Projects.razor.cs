using Fluxor;
using Kubernox.Shared.Interfaces;
using Kubernox.WebUi.States.Actions;
using Kubernox.WebUi.States.Stores;
using Microsoft.AspNetCore.Components;

namespace Kubernox.WebUi.Pages.Admin
{
    public partial class Projects
    {
        [Inject]
        public IHostClient HostClient { get; set; }

        [Inject]
        private IState<ProjectState> ProjectState { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; }


        protected override async Task OnInitializedAsync()
        {
            Dispatcher.Dispatch(new FetchProjectAction());
            await base.OnInitializedAsync();
        }
    }
}
