using Application.Exceptions;
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
    public class DomainNameController : ControllerBase
    {
        private readonly ILogger<DomainNameController> logger;
        private readonly IDomaineNameBusiness domaineNameBusiness;

        public DomainNameController(ILogger<DomainNameController> logger, IDomaineNameBusiness domaineNameBusiness)
        {
            if (domaineNameBusiness == null)
                throw new ArgumentNullException(nameof(domaineNameBusiness));

            this.logger = logger;
            this.domaineNameBusiness = domaineNameBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await domaineNameBusiness.ListDomainsAsync());
        }

        [HttpGet("cluster/{id}")]
        public async Task<IActionResult> GetClusterDomain(string clusterId)
        {
            return Ok(await domaineNameBusiness.ListDomainsForClusterAsync(clusterId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateDomainNameAsync([FromBody] DomainNameCreateRequest request)
        {
            try
            {
                if ((await domaineNameBusiness.CreateDomainAsync(request)))
                {
                    return Ok();
                }
            }
            catch (DuplicateException ex)
            {
                logger.LogWarning(ex, "Error on create domain.");
                return Conflict();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error on create domain.");
            }

            return BadRequest();
        }

        [HttpGet("{id}/validate")]
        public async Task<IActionResult> Validate([FromRoute] string id)
        {
            try
            {
                if ((await domaineNameBusiness.ValidateDomainAsync(id)))
                {
                    return Ok();
                }
            }
            catch (NotFoundException ex)
            {
                logger.LogWarning(ex, "Error on validate domain.");
                return NotFound();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error on validate domain.");
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDomainNameAsync([FromRoute] string id)
        {
            try
            {
                if ((await domaineNameBusiness.DeleteDomainAsync(id)))
                {
                    return Ok();
                }
            }
            catch (NotFoundException ex)
            {
                logger.LogWarning(ex, "Error on delete domain.");
                return NotFound();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error on delete domain.");
            }

            return BadRequest();
        }
    }
}
