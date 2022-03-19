using Kubernox.Infrastructure.Contracts.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kubernox.API.Controllers
{
    [ApiController]
    [Route("api/configuration")]
    [Authorize]
    public class ConfigurationController : ControllerBase
    {
        /// <summary>
        /// Get templates available
        /// </summary>
        /// <returns>Return template list from configuration</returns>
        /// <response code="200">Multiple template</response>
        [HttpGet("templates")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CoreResponse<TemplateListItemResponse[]>), StatusCodes.Status200OK)]
        public async Task<ActionResult<CoreResponse<TemplateListItemResponse[]>>> GetTemplatesAsync()
        {
            var templatesFile = await System.IO.File.ReadAllTextAsync($"Configurations/template.json");
            return Ok(new CoreResponse<TemplateListItemResponse[]>(true, JsonSerializer.Deserialize<TemplateListItemResponse[]>(templatesFile)));
        }
    }
}