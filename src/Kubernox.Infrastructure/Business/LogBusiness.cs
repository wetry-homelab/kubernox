using Kubernox.Infrastructure.Contracts.Response;
using Kubernox.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Kubernox.Infrastructure.Business
{
    public class LogBusiness : ILogBusiness
    {
        private readonly ILogger<LogBusiness> logger;
        private readonly ILogRepository logRepository;

        public LogBusiness(ILogger<LogBusiness> logger, ILogRepository logRepository)
        {
            this.logger = logger;
            this.logRepository = logRepository;
        }

        public async Task<LogResponse> GetLogsAsync(int start, int max)
        {
            var logs = await logRepository.ReadLogsAsync(start, max);
            var response = new LogResponse()
            {
                ItemCount = await logRepository.CountLogEntry(),
                Logs = logs.Select(s => new LogItemResponse()
                {
                    Id = s.Id,
                    Level = s.Level,
                    LogEvent = s.LogEvent,
                    MessageTemplate = s.MessageTemplate,
                    Exception = s.Exception,
                    TimeStamp = s.TimeStamp
                }).ToArray()
            };

            return response;
        }
    }
}
