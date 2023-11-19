using Kubernox.Shared.Interfaces;

using Microsoft.AspNetCore.Components;

namespace Kubernox.WebUi.Pages
{
    public partial class Home
    {
        [Inject]
        public IIdentityClient IdentityClient { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var data = await IdentityClient.TestAsync();
            await base.OnInitializedAsync();
        }
    }
}
