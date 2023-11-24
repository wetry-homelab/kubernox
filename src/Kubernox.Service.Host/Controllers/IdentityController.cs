using Kubernox.Application.Features.Identity.Commands;
using Kubernox.Shared;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kubernox.Service.Host.Controllers
{
    public class IdentityController : IdentityControllerBase
    {
        private readonly ILogger<IdentityController> logger;
        private readonly IMediator mediator;

        public IdentityController(ILogger<IdentityController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        [AllowAnonymous]
        public override async Task<ActionResult<SignInResponse>> SignIn([FromBody] SignInRequest body)
        {
            logger.LogInformation($"Start Sign In Flow.");
            var signInResult = await mediator.Send(new SignInCommand() { SignInRequest = body });

            if (signInResult != null)
                return Ok(signInResult);

            return Unauthorized();
        }
    }
}
