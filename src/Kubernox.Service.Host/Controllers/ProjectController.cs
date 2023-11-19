using Kubernox.Application.Features.Project.Commands;
using Kubernox.Application.Features.Project.Queries;
using Kubernox.Shared.Contracts.Request;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kubernox.Service.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProjectController : ControllerBase
    {
        private readonly ILogger<ProjectController> logger;
        private readonly IMediator mediator;

        public ProjectController(ILogger<ProjectController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProjectsAsync()
        {
            return Ok(await mediator.Send(new GetProjectQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProjectAsync([FromBody] CreateProjectRequest request)
        {
            var projectCreated = await mediator.Send(new CreateProjectCommand() { Request = request });

            if (projectCreated)
                return Created();

            return BadRequest();
        }
    }
}
