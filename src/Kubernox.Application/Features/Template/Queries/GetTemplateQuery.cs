using Kubernox.Shared;

using MediatR;

namespace Kubernox.Application.Features.Template.Queries
{
    public record GetTemplateQuery : IRequest<IEnumerable<TemplateItemResponse>>;
}
