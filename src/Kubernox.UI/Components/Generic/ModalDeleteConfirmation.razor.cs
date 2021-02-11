using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Kubernox.UI.Components.Generic
{
    public partial class ModalDeleteConfirmation : ComponentBase
    {
        public bool IsVisible { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> ValidateDelete { get; set; }

        public void ToggleModal()
        {
            IsVisible = !IsVisible;
        }
    }
}
