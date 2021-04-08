using AntDesign;
using Fluxor;
using Kubernox.UI.Core;
using Kubernox.UI.Store.Actions.DomainName;
using Kubernox.UI.Store.States;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Kubernox.UI.Pages.DomainSsl
{
    public partial class Index : CoreComponent
    {
        [Inject]
        private IState<DomainState> DomainState { get; set; }

        protected ITable table;
        protected bool visible = false;

        public bool IsLoad
        {
            get
            {
                return DomainState != null && DomainState.Value.IsLoading;
            }
        }

        protected override void OnInitialized()
        {
            DomainState.StateChanged += DomainNameState_StateChanged;
            Dispatcher.Dispatch(new FetchDomainNameAction());
            base.OnInitialized();
        }

        private void DomainNameState_StateChanged(object sender, DomainState e)
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
