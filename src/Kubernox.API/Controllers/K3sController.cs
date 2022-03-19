using Kubernox.Infrastructure.Contracts.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Kubernox.API.Controllers
{
    [ApiController]
    [Route("api/k3s")]
    [Authorize]
    public class K3sController : ControllerBase
    {
        /// <summary>
        /// Get K3S cluster list
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
