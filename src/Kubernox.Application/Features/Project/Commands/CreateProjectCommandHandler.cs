using Kubernox.Application.Interfaces;
using Kubernox.Infrastructure.Interfaces;

using MediatR;

namespace Kubernox.Application.Features.Project.Commands
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, bool>
    {
        private readonly IProjectNameService projectNameService;
        private readonly IProjectRepository projectRepository;

        public CreateProjectCommandHandler(IProjectNameService projectNameService, IProjectRepository projectRepository)
        {
            this.projectNameService = projectNameService;
            this.projectRepository = projectRepository;
        }

        public async Task<bool> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var projectDb = new Domain.Entities.Project()
            {
                Id = Guid.NewGuid(),
                Identifier = projectNameService.GenerateUniqueName(),
                Name = request.Request.Name
            };

            var insertProject = await projectRepository.AddProjectAsync(projectDb);
            return insertProject != 0;
        }
    }
}
