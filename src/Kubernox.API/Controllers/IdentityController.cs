using Kubernox.Infrastructure.Contracts.Request;
using Kubernox.Infrastructure.Contracts.Response;
using Kubernox.Infrastructure.Core.Exceptions;
using Kubernox.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Kubernox.API.Controllers
{
    [ApiController]
    [Route("api/identity")]
    [AllowAnonymous]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityBusiness identityBusiness;
        private readonly ILogger<IdentityController> logger;

        public IdentityController(IIdentityBusiness identityBusiness, ILogger<IdentityController> logger)
        {
            this.identityBusiness = identityBusiness;
            this.logger = logger;
        }

        /// <summary>
        /// Authenticate a user
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /identity
        ///     {
        ///        "username": "kubernox",
        ///        "passworx": "kubernox",
        ///     }
        ///
        /// </remarks>
        /// <param name="request"></param>
        /// <returns>A authenticate result</returns>
        /// <response code="200">Returns the user is successfully authorize</response>
        /// <response code="400">Cannot signin user</response>
        /// <response code="403">Cannot signin user</response>
        /// <response code="404">Cannot signin user</response>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CoreResponse<AuthenticationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CoreResponse<AuthenticationResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CoreResponse<AuthenticationResponse>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(CoreResponse<AuthenticationResponse>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CoreResponse<AuthenticationResponse>>> AuthenticateAsync([FromBody] AuthenticationRequest request)
        {
            try
            {
                var authenticateResult = await identityBusiness.AuthenticateUserAsync(request);

                if (authenticateResult.Token != null)
                {
                    return Ok(new CoreResponse<AuthenticationResponse>(true, authenticateResult));
                }

                return BadRequest();
            }
            catch (ForbidException fe)
            {
                logger.LogError(ExceptionCode.AuthenticateFailed, fe, $"Unable to authenticate user {request.Username}.", request.Username);
                return Forbid();
            }
            catch (NotFoundException)
            {
                logger.LogWarning(ExceptionCode.AuthenticateFailed, $"Unable to found user {request.Username}.", request);
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Update user password
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /identity
        ///     {
        ///        "id": "531645CB-290A-4200-AD34-52CA219EC768",
        ///        "username": "kubernox",
        ///        "passworx": "kubernox",
        ///        "passwordToken": "azeryuiop",
        ///        "repeatedPassword: "kubernox"
        ///     }
        ///
        /// </remarks>
        /// <param name="request"></param>
        /// <returns>A authenticate result</returns>
        /// <response code="200">Returns the user is successfully authorize with new password</response>
        /// <response code="400">Cannot change user password</response>
        /// <response code="403">Cannot change user password</response>
        /// <response code="404">Cannot change user password</response>
        [HttpPost("password-update")]
        [ProducesResponseType(typeof(CoreResponse<AuthenticationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CoreResponse<AuthenticationResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CoreResponse<AuthenticationResponse>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(CoreResponse<AuthenticationResponse>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePasswordAsync([FromBody] PasswordUpdateRequest request)
        {
            try
            {
                var updateResult = await identityBusiness.UpdatePasswordAsync(request);

                if (updateResult.Token != null)
                {
                    return Ok(new CoreResponse<AuthenticationResponse>(true, updateResult));
                }

                return BadRequest();
            }
            catch (ForbidException fe)
            {
                logger.LogError(ExceptionCode.AuthenticateFailed, fe, $"Unable to update password.", request);
                return Forbid();
            }
            catch (NotFoundException ne)
            {
                logger.LogWarning(ExceptionCode.AuthenticateFailed, ne, $"Unable to found user {request.Username}.", request);
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
