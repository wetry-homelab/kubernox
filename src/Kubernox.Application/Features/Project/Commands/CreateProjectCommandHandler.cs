using System.Text.Json;

using Kubernox.Application.Interfaces;
using Kubernox.Domain.Entities;
using Kubernox.Infrastructure.Interfaces;

using MediatR;

namespace Kubernox.Application.Features.Project.Commands
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, bool>
    {
        private readonly IProjectNameService projectNameService;
        private readonly IProjectRepository projectRepository;
        private readonly ILogRepository logRepository;

        public CreateProjectCommandHandler(IProjectNameService projectNameService, IProjectRepository projectRepository, ILogRepository logRepository)
        {
            this.projectNameService = projectNameService;
            this.projectRepository = projectRepository;
            this.logRepository = logRepository;
        }

        public async Task<bool> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var projectDb = new Domain.Entities.Project()
            {
                Id = Guid.NewGuid(),
                Identifier = projectNameService.GenerateUniqueName(),
                Name = request.Request.Name
            };

            var insertResult = await projectRepository.AddProjectAsync(projectDb);

            if (insertResult == 1)
                await logRepository.AddLogAsync(new Log() { Id = Guid.NewGuid(), CreateAt = DateTime.UtcNow, CreateBy = request.UserId, Data = $"New project with ID {projectDb.Id} insert.", Key = typeof(CreateProjectCommand).Name, Type = "Success" });
            else
                await logRepository.AddLogAsync(new Log() { Id = Guid.NewGuid(), CreateAt = DateTime.UtcNow, CreateBy = request.UserId, Data = $"Failed to insert project {JsonSerializer.Serialize(request.Request)}.", Key = typeof(CreateProjectCommand).Name, Type = "Error" });

            return insertResult != 0;
        }
    }
}
