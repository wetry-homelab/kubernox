using AntDesign;
using Infrastructure.Contracts.Request;
using Microsoft.AspNetCore.Components;

namespace Kubernox.UI.Pages.Ssh
{
    public partial class CreateModalComponent : Fluxor.Blazor.Web.Components.FluxorComponent
    {
        public string Layout { get; set; } = FormLayout.Vertical;

        [Parameter]
       public SshKeyCreateRequest SshKeyCreateRequest { get; set; }  = new SshKeyCreateRequest();

        void ToggleAutogenerate(bool value)
        {
            SshKeyCreateRequest.AutoGenerate = !SshKeyCreateRequest.AutoGenerate;
        }
    }
}
