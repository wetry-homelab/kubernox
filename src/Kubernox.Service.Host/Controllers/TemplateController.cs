using Kubernox.Application.Features.Template.Queries;
using Kubernox.Service.Host.Core;
using Kubernox.Shared;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kubernox.Service.Host.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TemplateController : TemplateControllerBase
    {
        private readonly ILogger<TemplateController> logger;
        private readonly IMediator mediator;

        public TemplateController(ILogger<TemplateController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        public override async Task<ActionResult<ICollection<TemplateItemResponse>>> ListTemplates()
        {
            var hosts = await mediator.Send(new GetTemplateQuery());
            return Ok(hosts);
        }
    }
}
