using Kubernox.Application.Features.Identity.Commands;
using Kubernox.Shared.Contracts.Request;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kubernox.Service.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class IdentityController : ControllerBase
    {
        private readonly ILogger<IdentityController> logger;
        private readonly IMediator mediator;

        public IdentityController(ILogger<IdentityController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SignInUserAsync([FromBody] SignInRequest request)
        {
            logger.LogInformation($"Start Sign In Flow.");
            var signInResult = await mediator.Send(new SignInCommand() { SignInRequest = request });

            if (signInResult != null)
                return Ok(signInResult);

            return Unauthorized();
        }
    }
}
