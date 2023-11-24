using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Kubernox.WebUi.Core;

namespace Kubernox.WebUi.Components.Shared
{
    public partial class TopHeader
    {
        [Inject]
        public SignOutSessionStateManager SignOutManager { get; set; }

        [Inject]
        public AuthenticationStateProvider AppAuthenticationStateProvider { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        private async Task LogoutAsync()
        {
            await SignOutManager.SetSignOutState();
            await ((KubernoxAuthenticationStateProvider)AppAuthenticationStateProvider).MarkUserAsLoggedOut();
            Navigation.NavigateTo("/login", true);
        }
    }
}
