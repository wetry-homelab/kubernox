using Kubernox.Shared;

using MediatR;

namespace Kubernox.Application.Features.Host.Queries
{
    public record GetHostQuery : IRequest<IEnumerable<HostItemResponse>>;

}
