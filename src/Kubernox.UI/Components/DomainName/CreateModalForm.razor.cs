using AntDesign;
using FluentValidation.Results;
using Infrastructure.Contracts.Request;
using Infrastructure.Validators;
using Kubernox.UI.Core;
using Kubernox.UI.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kubernox.UI.Components.DomainName
{
    public partial class CreateModalForm : CoreModalComponent
    {
        [Parameter]
        public DomainNameCreateRequest DomainNameCreateRequest { get; set; } = new DomainNameCreateRequest();

        [Inject]
        public IDomainNameService DomainNameService { get; set; }

        [Inject]
        public NotificationService NotificationService { get; set; }

        private DomainNameRequestValidator validator = new DomainNameRequestValidator();

        private ValidationResult validationResult;

        private string Layout { get; set; } = FormLayout.Vertical;

        private List<string> alreadyFocus = new List<string>();
        private string currentFocus;

        private bool loadInProgress;
        private bool displayError;

        private void OnFocus(string propertyName)
        {
            if (!alreadyFocus.Any(a => a == propertyName))
                alreadyFocus.Add(propertyName);

            currentFocus = propertyName;
        }

        private void OnBlur()
        {
            currentFocus = string.Empty;
        }

        private void Validate()
        {
            validationResult = validator.Validate(DomainNameCreateRequest);
        }

        protected override async Task HandleOk(MouseEventArgs e)
        {
            validationResult = validator.Validate(DomainNameCreateRequest);

            if (validationResult.IsValid)
            {
                displayError = false;

                loadInProgress = true;

                var claimResult = await DomainNameService.CreateDomainNameAsync(DomainNameCreateRequest);

                if (claimResult)
                {
                    DomainNameCreateRequest = new DomainNameCreateRequest();

                    await OnCloseCallback.InvokeAsync(e);

                    await NotificationService.Open(new NotificationConfig()
                    {
                        Message = Translator.GetString("CLAIM_SUCCESS_NOTIFICATION_TITLE").Value,
                        Description = Translator.GetString("CLAIM_SUCCESS_NOTIFICATION_CONTENT").Value,
                        NotificationType = NotificationType.Success
                    });
                }
                else
                {
                    displayError = true;
                }

                loadInProgress = false;
            }
        }

        protected override async Task HandleCancel(MouseEventArgs e)
        {
            await OnCloseCallback.InvokeAsync(e);
            StateHasChanged();
        }
    }
}
