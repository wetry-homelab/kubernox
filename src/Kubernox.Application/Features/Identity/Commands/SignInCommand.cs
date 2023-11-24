using Kubernox.Shared;

using MediatR;

namespace Kubernox.Application.Features.Identity.Commands
{
    public class SignInCommand : IRequest<SignInResponse?>
    {
        public SignInRequest SignInRequest { get; set; }
    }
}
