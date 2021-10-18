using AntDesign;
using Fluxor;
using Infrastructure.Contracts.Request;
using Infrastructure.Contracts.Response;
using Kubernox.UI.Core;
using Kubernox.UI.Services.Interfaces;
using Kubernox.UI.Store.Actions.DomainName;
using Kubernox.UI.Store.States;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kubernox.UI.Components.ClusterDetails
{
    public partial class LinkDomainForm : CoreComponent
    {
        [Inject]
        private IState<DomainState> DomainState { get; set; }

        private List<string> resolvers = new List<string>() {
            "HTTP", "OVH", "GANDIV5"
        };

        public string Layout { get; set; } = FormLayout.Vertical;

        [Inject]
        public IDomainService DomainService { get; set; }

        [Parameter]
        public string ClusterId { get; set; }

        [Inject]
        public NotificationService NotificationService { get; set; }

        public DomainLinkingRequestContract DomainLinkingRequestContract { get; set; } = new DomainLinkingRequestContract();

        private bool isLoading;

        protected override void OnInitialized()
        {
            DomainState.StateChanged += DomainNameState_StateChanged;
            base.OnInitialized();
        }

        private void DomainNameState_StateChanged(object sender, DomainState e)
        {
            StateHasChanged();
        }

        private void OnSelectedItemChangedHandler(string value)
        {
            DomainLinkingRequestContract.DomainId = value;
        }

        private bool DisableCertificateField()
        {
            return DomainLinkingRequestContract != null && DomainLinkingRequestContract.Resolver != "none";
        }

        private bool DiplayCertificateField()
        {
            return DomainLinkingRequestContract != null && (DomainLinkingRequestContract.Resolver == "none") && DomainLinkingRequestContract.Protocol != "HTTP";
        }

        public async Task LinkClusterDomainAsync()
        {
            isLoading = true;
            DomainLinkingRequestContract.ClusterId = ClusterId;

            if (await DomainService.LinkDomainToClusterAsync(DomainLinkingRequestContract))
            {
                Dispatcher.Dispatch(new FetchClusterDomainNameAction(ClusterId));
                isLoading = false;

                await NotificationService.Open(new NotificationConfig()
                {
                    Message = Translator.GetString("LINK_DOMAIN_SUCCESS_NOTIFICATION_TITLE").Value,
                    Description = Translator.GetString("LINK_DOMAIN_SUCCESS_NOTIFICATION_CONTENT").Value,
                    NotificationType = NotificationType.Success
                });
            }
            else
            {
                isLoading = false;
                await NotificationService.Open(new NotificationConfig()
                {
                    Message = Translator.GetString("LINK_DOMAIN_ERROR_NOTIFICATION_TITLE").Value,
                    Description = Translator.GetString("LINK_DOMAIN_ERROR_NOTIFICATION_CONTENT").Value,
                    NotificationType = NotificationType.Error
                });
            }
        }
    }
}
