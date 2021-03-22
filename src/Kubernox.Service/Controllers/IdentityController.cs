using Application.Interfaces;
using Infrastructure.Contracts.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Kubernox.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly ILogger<IdentityController> logger;
        private readonly IIdentityBusiness identityBusiness;

        public IdentityController(ILogger<IdentityController> logger, IIdentityBusiness identityBusiness)
        {
            if (identityBusiness == null)
                throw new ArgumentNullException(nameof(identityBusiness));

            this.logger = logger;
            this.identityBusiness = identityBusiness;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] AuthRequest request)
        {
            var authResult = await identityBusiness.AuthenticateAsync(request);

            if (authResult == null || !authResult.Success)
                return Forbid();

            return Ok(authResult);
        }
    }
}
