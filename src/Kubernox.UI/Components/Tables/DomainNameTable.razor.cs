using AntDesign;
using Fluxor;
using Kubernox.UI.Services.Interfaces;
using Kubernox.UI.Store.Actions.DomainName;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace Kubernox.UI.Components.Tables
{
    public partial class DomainNameTable : Fluxor.Blazor.Web.Components.FluxorComponent
    {

        [Inject]
        public IDomainNameService DomainNameService { get; set; }
        
        [Inject]
        IDispatcher Dispatcher { get; set; }

        [Inject]
        public NotificationService NotificationService { get; set; }

        public bool ValidationInProgress { get; set; }

        public bool ValidationResult { get; set; }

        public bool DisplayError { get; set; }

        protected ITable table;

        private async Task StartValidationAsync(string id)
        {
            ValidationInProgress = true;

            if (await DomainNameService.ValidateDomainNameAsync(id))
            {
                Dispatcher.Dispatch(new FetchDomainNameAction());
                ValidationInProgress = false;

                await NotificationService.Open(new NotificationConfig()
                {
                    Message = Translator.GetString("CLAIM_SUCCESS_NOTIFICATION_TITLE").Value,
                    Description = Translator.GetString("DOMAIN_VALIDATION_FAILED").Value,
                    NotificationType = NotificationType.Success
                });
            }
            else
            {
                ValidationInProgress = false;
                await NotificationService.Open(new NotificationConfig()
                {
                    Message = Translator.GetString("CLAIM_SUCCESS_NOTIFICATION_TITLE").Value,
                    Description = Translator.GetString("DOMAIN_VALIDATION_SUCCESS").Value,
                    NotificationType = NotificationType.Error
                });
            }
        }
    }
}
