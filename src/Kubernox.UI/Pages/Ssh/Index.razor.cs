using AntDesign;
using Fluxor;
using Infrastructure.Contracts.Request;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Services;
using Kubernox.UI.Store.Actions.SshKey;
using Kubernox.UI.Store.States;
using System.Threading.Tasks;
using Kubernox.UI.Utils;

namespace Kubernox.UI.Pages.Ssh
{
    public partial class Index : Fluxor.Blazor.Web.Components.FluxorComponent
    {
        [Inject]
        private IState<SshKeyState> SshKeyState { get; set; }

        [Inject]
        IDispatcher Dispatcher { get; set; }

        [Inject]
        SshKeyService SshKeyService { get; set; }

        [Inject]
        IJSRuntime JSRuntime { get; set; }

        public string Layout { get; set; } = FormLayout.Vertical;

        protected ITable table;
        protected bool visible = false;

        protected SshKeyCreateRequest sshKeyCreateRequest = new SshKeyCreateRequest();

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

        void ToggleAutogenerate(bool value)
        {
            sshKeyCreateRequest.AutoGenerate = !sshKeyCreateRequest.AutoGenerate;
        }

        protected async Task DeleteAsync(int id)
        {
            Dispatcher.Dispatch(new DeleteSshKeyAction(id));
        }

        protected async Task DownloadPem(int id)
        {
            var download = await SshKeyService.DownloadKeyAsync(id, "PEM");
            await FileUtil.SaveAs(JSRuntime, download.Name, System.Text.Encoding.UTF8.GetBytes(download.Content));
        }

        protected async Task DownloadPpk(int id)
        {
            var download = await SshKeyService.DownloadKeyAsync(id, "PPK");
            await FileUtil.SaveAs(JSRuntime, download.Name, System.Text.Encoding.UTF8.GetBytes(download.Content));
        }

        private void HandleOk(MouseEventArgs e)
        {
            Dispatcher.Dispatch(new CreateSshKeyAction(sshKeyCreateRequest));
            visible = false;
        }

        private void HandleCancel(MouseEventArgs e)
        {
            visible = false;
        }
    }
}
