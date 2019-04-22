using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebSiteParser.Controllers.Requests;
using WebSiteParser.Requests;

namespace WebSiteParser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ParserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("")]
        public async Task<IActionResult> ParseWebSite([FromBody] ParseRequest request)
        {
            if (!Uri.TryCreate(request.Url, UriKind.Absolute, out var uri))
                return new BadRequestObjectResult(request.Url);

            var options = new Options(request.ParsingRules,
                request.ContentValidationRules, request.ResponseValidationRules);

            var result = await _mediator.Send(new ParseWebSiteRequest(uri,
                request.MaxDepth == 0 ? 1 : request.MaxDepth, options));

            return new OkObjectResult(result);
        }
    }
}