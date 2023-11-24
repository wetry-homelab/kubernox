using Kubernox.Application.Features.Project.Commands;
using Kubernox.Application.Features.Project.Queries;
using Kubernox.Shared;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kubernox.Service.Host.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProjectController : ProjectControllerBase
    {
        private readonly ILogger<ProjectController> logger;
        private readonly IMediator mediator;

        public ProjectController(ILogger<ProjectController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        public override async Task<ActionResult<ICollection<ProjectItemResponse>>> ListProjects()
        {
            return Ok(await mediator.Send(new GetProjectQuery()));
        }

        public override async Task<ActionResult<ProjectItemResponse>> CreateProject([FromBody] ProjectRequest body)
        {
            var projectCreated = await mediator.Send(new CreateProjectCommand() { Request = body });

            if (projectCreated)
                return Created();

            return BadRequest();
        }
    }
}
