using FluentValidation;
using Infrastructure.Contracts.Request;
using System;
using System.Text.RegularExpressions;

namespace Infrastructure.Validators
{
    public class DomainNameRequestValidator : AbstractValidator<DomainNameCreateRequest>
    {
        public DomainNameRequestValidator()
        {
            RuleFor(r => r.RootDomain)
                    .NotEmpty()
                    .WithMessage("#FIELD_REQUIRED#")
                    .NotNull()
                    .WithMessage("#FIELD_REQUIRED#")
                    .MinimumLength(4)
                    .WithMessage("#FIELD_MIN_LENGTH#")
                    .MaximumLength(128)
                    .WithMessage("#FIELD_MAX_LENGTH#")
                    .Must(LinkMustBeAUri)
                    .WithMessage("#FIELD_FORMAT_ERROR");
        }

        private static bool LinkMustBeAUri(string link)
        {
            if (string.IsNullOrWhiteSpace(link))
            {
                return false;
            }

            Regex regex = new Regex("[A-z0-9-]+.[A-z]{2,8}");
            return regex.IsMatch(link);
        }
    }
}
