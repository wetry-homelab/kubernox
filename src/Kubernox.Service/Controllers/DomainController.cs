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
    public class DomainController : ControllerBase
    {
        private readonly ILogger<DomainController> logger;
        private readonly IDomainBusiness domaineBusiness;

        public DomainController(ILogger<DomainController> logger, IDomainBusiness domaineBusiness)
        {
            if (domaineBusiness == null)
                throw new ArgumentNullException(nameof(domaineBusiness));

            this.logger = logger;
            this.domaineBusiness = domaineBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await domaineBusiness.ListDomainsAsync());
        }

        [HttpGet("cluster/{clusterId}")]
        public async Task<IActionResult> GetClusterDomain(string clusterId)
        {
            return Ok(await domaineBusiness.ListDomainsForClusterAsync(clusterId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateDomainNameAsync([FromBody] DomainNameCreateRequest request)
        {
            try
            {
                if ((await domaineBusiness.CreateDomainAsync(request)))
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
                if ((await domaineBusiness.ValidateDomainAsync(id)))
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

        [HttpPost("link-to-cluster")]
        public async Task<IActionResult> Link([FromBody] DomainLinkingRequestContract request)
        {
            try
            {
                if ((await domaineBusiness.LinkDomainToClusterAsync(request)))
                {
                    return Ok();
                }
            }
            catch (DuplicateException dex)
            {
                logger.LogWarning(dex, "Error on link domain.");
                return Conflict();
            }
            catch (NotFoundException ex)
            {
                logger.LogWarning(ex, "Error on link domain.");
                return NotFound();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error on link domain.");
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDomainNameAsync([FromRoute] string id)
        {
            try
            {
                if ((await domaineBusiness.DeleteDomainAsync(id)))
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
