using FluentValidation.Results;
using Infrastructure.Contracts.Request;
using Infrastructure.Validators;

namespace Kubernox.UI.Components.Modals
{
    public partial class ClaimRootDomainModal : Fluxor.Blazor.Web.Components.FluxorComponent
    {
        public DomainNameRequestValidator Validator { get; set; } = new DomainNameRequestValidator();

        public DomainNameCreateRequest DomainNameCreateRequest { get; set; } = new DomainNameCreateRequest();

        private ValidationResult validationResult;

        private void Validate()
        {
            validationResult = Validator.Validate(DomainNameCreateRequest);
        }
    }
}
