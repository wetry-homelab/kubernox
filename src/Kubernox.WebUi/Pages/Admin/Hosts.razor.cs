using AntDesign;

using Fluxor;

using Kubernox.Shared;
using Kubernox.WebUi.Components.Modals;
using Kubernox.WebUi.States.Actions;
using Kubernox.WebUi.States.Stores;

using Microsoft.AspNetCore.Components;

namespace Kubernox.WebUi.Pages.Admin
{
    public partial class Hosts
    {
        [Inject]
        public IKubernoxClient KubernoxClient { get; set; }

        [Inject]
        private IState<HostState> HostState { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; }

        [Inject]
        public ModalService ModalService { get; set; }

        private CreateHostConfigurationModal createHostConfigurationModal;

        private async Task OpenCreateHostConfigurationAsync()
        {
            createHostConfigurationModal?.ToggleModal();
        }

        protected override async Task OnInitializedAsync()
        {
            Dispatcher.Dispatch(new FetchHostAction());
            await base.OnInitializedAsync();
        }
    }
}
