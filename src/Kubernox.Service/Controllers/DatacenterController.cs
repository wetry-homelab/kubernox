using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Kubernox.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DatacenterController : ControllerBase
    {
        private readonly ILogger<DatacenterController> logger;
        private readonly IDatacenterBusiness datacenterBusiness;

        public DatacenterController(ILogger<DatacenterController> logger, IDatacenterBusiness datacenterBusiness)
        {
            if (datacenterBusiness == null)
                throw new ArgumentNullException(nameof(datacenterBusiness));

            this.logger = logger;
            this.datacenterBusiness = datacenterBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await datacenterBusiness.GetDatacenter());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var node = await datacenterBusiness.GetDatacenterNode(id);

            if (node == null)
                return NotFound();

            return Ok(node);
        }
    }
}
