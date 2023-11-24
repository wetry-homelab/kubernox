using Kubernox.Infrastructure.Interfaces;
using Kubernox.Shared;

using MediatR;

namespace Kubernox.Application.Features.Project.Queries
{
    public class GetProjectQueryHandler : IRequestHandler<GetProjectQuery, IEnumerable<ProjectItemResponse>>
    {
        private readonly IProjectRepository projectRepository;

        public GetProjectQueryHandler(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public async Task<IEnumerable<ProjectItemResponse>> Handle(GetProjectQuery request, CancellationToken cancellationToken)
        {
            var projects = await projectRepository.GetProjectsAsync();
            return projects.Select(s => new ProjectItemResponse()
            {
                Id = s.Id,
                Identifier = s.Identifier,
                Name = s.Name
            }).ToList();
        }
    }
}
