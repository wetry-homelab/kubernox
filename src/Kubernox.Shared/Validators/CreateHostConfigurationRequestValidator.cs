using FluentValidation;


namespace Kubernox.Shared.Validators
{
    public class CreateHostConfigurationRequestValidator : AbstractValidator<HostConfigurationRequest>
    {
        public CreateHostConfigurationRequestValidator()
        {
            RuleFor(r => r.Name).NotEmpty().NotNull();
            RuleFor(r => r.Ip).NotEmpty().NotNull().Matches("^(?:[0-9]{1,3}\\.){3}[0-9]{1,3}$");
            RuleFor(r => r.ApiToken).NotEmpty().NotNull();
        }
    }
}
