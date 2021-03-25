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
        private IState<DomainNameState> DomainNameState { get; set; }

        [Inject]
        IDispatcher Dispatcher { get; set; }

        protected ITable table;
        protected bool visible = false;

        public bool IsLoad
        {
            get
            {
                return DomainNameState != null && DomainNameState.Value.IsLoading;
            }
        }

        protected override void OnInitialized()
        {
            DomainNameState.StateChanged += DomainNameState_StateChanged;
            Dispatcher.Dispatch(new FetchDomainNameAction());
            base.OnInitialized();
        }

        private void DomainNameState_StateChanged(object sender, DomainNameState e)
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
