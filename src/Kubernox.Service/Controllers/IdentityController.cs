using Application.Interfaces;
using Infrastructure.Contracts.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Kubernox.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly ILogger<IdentityController> logger;
        private readonly IIdentityBusiness identityBusiness;

        public IdentityController(ILogger<IdentityController> logger, IIdentityBusiness identityBusiness)
        {
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
