using AntDesign;
using Fluxor;
using Infrastructure.Contracts.Request;
using Infrastructure.Contracts.Response;
using Kubernox.UI.Store.States;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace Kubernox.UI.Components.ClusterDetails
{
    public partial class LinkDomainForm : ComponentBase
    {
        [Inject]
        private IState<DomainNameState> DomainNameState { get; set; }

        private List<string> resolvers = new List<string>() {
            "HTTP", "OVH", "GANDIV5"
        };

        public string Layout { get; set; } = FormLayout.Vertical;

        [Inject]
        IDispatcher Dispatcher { get; set; }

        [Parameter]
        public string ClusterId { get; set; }

        [Parameter]
        public ClusterDomainItemResponse Entry { get; set; }

        public DomainLinkingRequestContract DomainLinkingRequestContract { get; set; } = new DomainLinkingRequestContract();

        protected override void OnInitialized()
        {
            DomainNameState.StateChanged += DomainNameState_StateChanged;
            //Dispatcher.Dispatch(new FetchDomainNameAction());
            base.OnInitialized();
        }

        private void DomainNameState_StateChanged(object sender, DomainNameState e)
        {
            StateHasChanged();
        }

        private void OnSelectedItemChangedHandler(string value)
        {
            DomainLinkingRequestContract.DomainNameId = value;
        }

        private bool DisableCertificateField()
        {
            return DomainLinkingRequestContract != null && DomainLinkingRequestContract.Resolver != "none";
        }

        private bool DiplayCertificateField()
        {
            return DomainLinkingRequestContract != null && (DomainLinkingRequestContract.Resolver == "none");
        }

        protected override void OnParametersSet()
        {
            if (Entry != null)
            {
                DomainLinkingRequestContract = new DomainLinkingRequestContract()
                {
                    SubDomain = Entry.Value,
                    DomainNameId = Entry.RootDomainId,
                    Resolver = Entry.Resolver,
                    ClusterId = Entry.ClusterId
                };
            }
            else
            {
                DomainLinkingRequestContract = new DomainLinkingRequestContract()
                {
                    ClusterId = ClusterId
                };
            }

            base.OnParametersSet();
        }
    }
}
