using Microsoft.AspNetCore.Components;

namespace Kubernox.WebUi
{
    public partial class App
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
    }
}
