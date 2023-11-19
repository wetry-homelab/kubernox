using Kubernox.Shared.Contracts.Request;

using MediatR;

namespace Kubernox.Application.Features.Project.Commands
{
    public class CreateProjectCommand : IRequest<bool>
    {
        public CreateProjectRequest Request { get; set; }
    }
}
