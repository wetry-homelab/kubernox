using Kubernox.Application.Features.Host.Queries;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kubernox.Service.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class HostController : ControllerBase
    {
        private readonly ILogger<HostController> logger;
        private readonly IMediator mediator;

        public HostController(ILogger<HostController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetClustersListAsync()
        {
            var clusters = await mediator.Send(new GetHostQuery());
            return Ok(clusters);
        }
    }
}
