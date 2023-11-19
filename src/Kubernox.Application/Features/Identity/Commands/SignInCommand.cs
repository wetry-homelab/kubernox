using Kubernox.Shared.Contracts.Request;
using Kubernox.Shared.Contracts.Response;

using MediatR;

namespace Kubernox.Application.Features.Identity.Commands
{
    public class SignInCommand : IRequest<SignInResponse?>
    {
        public SignInRequest SignInRequest { get; set; }
    }
}
