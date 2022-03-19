using Kubernox.Infrastructure.Contracts.Response;
using Kubernox.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Kubernox.API.Controllers
{
    [ApiController]
    [Route("api/log")]
    //[Authorize]
    public class LogController : ControllerBase
    {
        private readonly ILogBusiness logBusiness;
        private readonly ILogger<LogController> logger;

        public LogController(ILogBusiness logBusiness, ILogger<LogController> logger)
        {
            this.logBusiness = logBusiness;
            this.logger = logger;
        }

        /// <summary>
        /// Get application logs
        /// </summary>
        /// <returns>Return the application logs and pagination</returns>
        /// <response code="200">Multiple log line and pagination</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CoreResponse<LogResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CoreResponse<LogResponse>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CoreResponse<LogResponse>>> GetLogsAsync([FromQuery] int start = 0, [FromQuery] int max = 10, [FromQuery] string query = null)
        {
            var logs = await logBusiness.GetLogsAsync(start, max);
            return Ok(new CoreResponse<LogResponse>(true, logs));
        }
    }
}
