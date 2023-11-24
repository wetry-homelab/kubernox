using Kubernox.Application.Features.Host.Commands;
using Kubernox.Application.Features.Host.Queries;
using Kubernox.Service.Host.Core;
using Kubernox.Shared;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kubernox.Service.Host.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class HostController : HostControllerBase
    {
        private readonly ILogger<HostController> logger;
        private readonly IMediator mediator;

        public HostController(ILogger<HostController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }


        public override async Task<ActionResult<ICollection<HostItemResponse>>> ListHosts()
        {
            var hosts = await mediator.Send(new GetHostQuery());
            return Ok(hosts);
        }

        public override async Task<ActionResult<HostItemResponse>> CreateHost([FromBody] HostConfigurationRequest body)
        {
            var created = await mediator.Send(new CreateHostCommand()
            {
                Request = body,
                UserId = UserId
            });

            return created ? Created() : BadRequest();
        }
    }
}
