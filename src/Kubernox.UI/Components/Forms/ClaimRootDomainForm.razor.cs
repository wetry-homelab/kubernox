using AntDesign;
using FluentValidation.Results;
using Infrastructure.Contracts.Request;
using Infrastructure.Validators;
using Microsoft.AspNetCore.Components;

namespace Kubernox.UI.Components.Forms
{
    public partial class ClaimRootDomainForm : Fluxor.Blazor.Web.Components.FluxorComponent
    {
        public string Layout { get; set; } = FormLayout.Vertical;

        [Parameter]
        public DomainNameCreateRequest DomainNameCreateRequest { get; set; } = new DomainNameCreateRequest();

        private DomainNameRequestValidator validator = new DomainNameRequestValidator();

        private ValidationResult validationResult;

        private void Validate()
        {
            validationResult = validator.Validate(DomainNameCreateRequest);
        }
    }
}
