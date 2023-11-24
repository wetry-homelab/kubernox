using System.Text.Json;

using Kubernox.Domain.Entities;
using Kubernox.Infrastructure.Interfaces;

using MediatR;

namespace Kubernox.Application.Features.Host.Commands
{
    public class CreateHostCommandHandler : IRequestHandler<CreateHostCommand, bool>
    {
        private readonly IHostConfigurationRepository hostConfigurationRepository;
        private readonly ILogRepository logRepository;

        public CreateHostCommandHandler(IHostConfigurationRepository hostConfigurationRepository, ILogRepository logRepository)
        {
            this.hostConfigurationRepository = hostConfigurationRepository;
            this.logRepository = logRepository;
        }

        public async Task<bool> Handle(CreateHostCommand request, CancellationToken cancellationToken)
        {
            var hostDb = new HostConfiguration()
            {
                ApiToken = request.Request.ApiToken,
                CreateAt = DateTime.UtcNow,
                CreateBy = request.UserId,
                Ip = request.Request.Ip,
                Id = Guid.NewGuid(),
                Name = request.Request.Name,
                IsActive = true
            };

            var insertResult = await hostConfigurationRepository.AddHostConfigurationAsync(hostDb);

            if (insertResult == 1)
                await logRepository.AddLogAsync(new Log() { Id = Guid.NewGuid(), CreateAt = DateTime.UtcNow, CreateBy = request.UserId, Data = $"New host with ID {hostDb.Id} insert.", Key = typeof(CreateHostCommand).Name, Type = "Success" });
            else
                await logRepository.AddLogAsync(new Log() { Id = Guid.NewGuid(), CreateAt = DateTime.UtcNow, CreateBy = request.UserId, Data = $"Failed to insert host {JsonSerializer.Serialize(request.Request)}.", Key = typeof(CreateHostCommand).Name, Type = "Error" });

            return insertResult == 1;
        }
    }
}
