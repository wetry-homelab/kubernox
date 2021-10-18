using Fluxor;
using Kubernox.UI.Store.Actions.SshKey;
using Kubernox.UI.Store.States;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Kubernox.UI.Pages.Ssh
{
    public partial class Index : Fluxor.Blazor.Web.Components.FluxorComponent
    {
        [Inject]
        private IState<SshKeyState> SshKeyState { get; set; }

        [Inject]
        IDispatcher Dispatcher { get; set; }

        protected bool visible = false;

        protected override void OnInitialized()
        {
            SshKeyState.StateChanged += SshKeyState_StateChanged;
            Dispatcher.Dispatch(new FetchSshKeyAction());
            base.OnInitialized();
        }

        private void SshKeyState_StateChanged(object sender, SshKeyState e)
        {
            StateHasChanged();
        }

        private void HideModal(MouseEventArgs e)
        {
            visible = false;
            StateHasChanged();
        }
    }
}
