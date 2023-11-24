using AntDesign;

using Blazored.LocalStorage;

using FluentValidation.Results;

using Kubernox.Shared;
using Kubernox.Shared.Validators;
using Kubernox.WebUi.Core;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;


namespace Kubernox.WebUi.Pages.Identity
{
    public partial class LoginPage
    {
        [Inject]
        public IKubernoxClient KubernoxClient { get; set; }

        [Inject]
        public ILocalStorageService LocalStorage { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public AuthenticationStateProvider AppAuthenticationStateProvider { get; set; }

        private SignInRequest signInRequest = new SignInRequest();
        private SignInRequestValidator signInRequestValidator = new SignInRequestValidator();
        private ValidationResult validationResult = null;

        private bool authError;
        private bool isLoading;

        public async Task SignInAsync()
        {
            isLoading = true;
            validationResult = await signInRequestValidator.ValidateAsync(signInRequest);
            if (validationResult.IsValid)
            {
                var authResult = await KubernoxClient.SignInAsync(signInRequest);
                if (authResult != null)
                {
                    authError = string.IsNullOrEmpty(authResult.AccessToken);

                    if (!authError)
                    {
                        await LocalStorage.SetItemAsStringAsync("access_token", authResult.AccessToken);
                        await ((KubernoxAuthenticationStateProvider)AppAuthenticationStateProvider).MarkUserAsAuthenticated();
                        NavigationManager.NavigateTo("/", false);
                    }
                }
                else
                {
                    authError = true;
                }
            }

            isLoading = false;
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
