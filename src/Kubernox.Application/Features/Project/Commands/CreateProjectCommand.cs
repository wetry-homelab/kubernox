
using Kubernox.Shared;

using MediatR;

namespace Kubernox.Application.Features.Project.Commands
{
    public class CreateProjectCommand : IRequest<bool>
    {
        public ProjectRequest Request { get; set; }
        public string UserId { get; set; }
    }
}
