using AntDesign;

using FluentValidation.Results;

using Fluxor;

using Kubernox.Shared;
using Kubernox.Shared.Validators;
using Kubernox.WebUi.States.Actions;

using Microsoft.AspNetCore.Components;

namespace Kubernox.WebUi.Components.Modals
{
    public partial class CreateHostConfigurationModal
    {

        [Inject]
        public IKubernoxClient KubernoxClient { get; set; }

        [Inject]
        public NotificationService NotificationService { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; }

        private HostConfigurationRequest request = new HostConfigurationRequest();
        private CreateHostConfigurationRequestValidator createHostConfigurationRequestValidator = new CreateHostConfigurationRequestValidator();
        private ValidationResult validationResult = null;

        private bool showModal;
        private bool createError;
        private bool isLoading;

        private async Task CreateHostConfigurationAsync()
        {
            isLoading = true;
            validationResult = await createHostConfigurationRequestValidator.ValidateAsync(request);
            if (validationResult.IsValid)
            {
                var createResult = await KubernoxClient.CreateHostAsync(request);
                if (createResult != null)
                {
                    NotificationService.Open(new NotificationConfig()
                    {
                        NotificationType = NotificationType.Success,
                        Description = "Host created."
                    });

                    Dispatcher.Dispatch(new FetchHostAction());
                    showModal = false;
                }
                else
                {
                    createError = true;
                }
            }

            StateHasChanged();
            isLoading = false;
        }

        public void ToggleModal()
        {
            showModal = !showModal;
            StateHasChanged();
        }



        public FormValidateStatus GetValidationStatus(string propertyName)
        {
            if (validationResult == null)
                return FormValidateStatus.Validating;

            if (validationResult.Errors.Any(f => f.PropertyName == propertyName))
                return FormValidateStatus.Error;

            return FormValidateStatus.Success;
        }
    }
}
