using Kubernox.Shared.Contracts.Request;

using MediatR;

namespace Kubernox.Application.Features.Project.Queries
{
    public record GetProjectQuery : IRequest<IEnumerable<ProjectItemResponse>>;
}
