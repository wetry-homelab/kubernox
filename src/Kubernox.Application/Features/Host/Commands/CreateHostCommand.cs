using Kubernox.Shared;

using MediatR;

namespace Kubernox.Application.Features.Host.Commands
{
    public class CreateHostCommand : IRequest<bool>
    {
        public HostConfigurationRequest Request { get; set; }
        public string UserId { get; set; }
    }
}
