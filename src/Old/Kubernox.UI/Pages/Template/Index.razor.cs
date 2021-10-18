using AntDesign;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Kubernox.UI.Store.Actions.Template;
using Kubernox.UI.Store.States;

namespace Kubernox.UI.Pages.Template
{
    public partial class Index : Fluxor.Blazor.Web.Components.FluxorComponent
    {
        [Inject]
        private IState<TemplateState> TemplateState { get; set; }

        [Inject]
        IDispatcher Dispatcher { get; set; }
        
        protected ITable table;
        protected bool visible = false;

        protected override void OnInitialized()
        {
            TemplateState.StateChanged += TemplateState_StateChanged;
            Dispatcher.Dispatch(new FetchTemplateAction());
            base.OnInitialized();
        }

        private void TemplateState_StateChanged(object sender, TemplateState e)
        {
            StateHasChanged();
        }
    }
}
