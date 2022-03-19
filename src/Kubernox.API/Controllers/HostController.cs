using Kubernox.Infrastructure.Contracts.Request;
using Kubernox.Infrastructure.Contracts.Response;
using Kubernox.Infrastructure.Core.Exceptions;
using Kubernox.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kubernox.API.Controllers
{
    [ApiController]
    [Route("api/host")]
    //[Authorize]
    public class HostController : ControllerBase
    {
        private readonly IHostBusiness hostBusiness;
        private readonly ILogger<HostController> logger;

        public HostController(IHostBusiness hostBusiness, ILogger<HostController> logger)
        {
            this.hostBusiness = hostBusiness;
            this.logger = logger;
        }

        /// <summary>
        /// Create a new host
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /host
        ///     {
        ///        "user": "kubernox",
        ///        "token": "012345849",
        ///        "name": "dev",
        ///        "ip": "127.0.0.1",
        ///     }
        ///
        /// </remarks>
        /// <param name="request"></param>
        /// <returns>Return the GUID of the new created host</returns>
        /// <response code="201">Returns new host ID</response>
        /// <response code="400"></response>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CoreResponse<Guid?>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CoreResponse<Guid?>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CoreResponse<Guid?>), StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CoreResponse<Guid?>>> CreateHostAsync([FromBody] HostCreateRequest request)
        {
            try
            {
                var hostId = await hostBusiness.CreateHostAsync(request);
                if (hostId != null)
                    return Created("", new CoreResponse<Guid?>(true, hostId));

                return BadRequest();
            }
            catch (DuplicateException e)
            {
                return Conflict(new CoreResponse<Guid?>(false, e.Message, e.Message));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Get full list of host
        /// </summary>
        /// <returns>Return a list of host</returns>
        /// <response code="200">Returns host list</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CoreResponse<IEnumerable<HostListItemResponse>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<CoreResponse<IEnumerable<HostListItemResponse>>>> GetHostsAsync()
        {
            return Ok(new CoreResponse<IEnumerable<HostListItemResponse>>(true, await hostBusiness.GetHostListAsync()));
        }
    }
}
