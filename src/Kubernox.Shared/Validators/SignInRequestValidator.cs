using FluentValidation;

using Kubernox.Shared.Contracts.Request;

namespace Kubernox.Shared.Validators
{
    public class SignInRequestValidator : AbstractValidator<SignInRequest>
    {
        public SignInRequestValidator()
        {
            RuleFor(r => r.Username).NotEmpty().NotNull();
            RuleFor(r => r.Password).NotEmpty().NotNull();
        }
    }
}
