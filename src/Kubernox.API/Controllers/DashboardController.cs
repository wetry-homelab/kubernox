using Kubernox.Infrastructure.Contracts.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Kubernox.API.Controllers
{
    [ApiController]
    [Route("api/dashboard")]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        /// <summary>
        /// Get dashboard data
        /// </summary>
        /// <returns>Return dashboard data</returns>
        /// <response code="200">Multiple chart data dashboard</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CoreResponse<object>), StatusCodes.Status200OK)]
        public async Task<ActionResult<CoreResponse<object>>> GetHostsAsync()
        {
            return Ok(new CoreResponse<object>(true, new { }));
        }
    }
}
