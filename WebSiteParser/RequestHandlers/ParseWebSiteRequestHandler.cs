using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebSiteParser.Controllers.Responses;
using WebSiteParser.Requests;

namespace WebSiteParser.RequestHandlers
{
    public class ParseWebSiteRequestHandler : IRequestHandler<ParseWebSiteRequest, PageResult>
    {
        private readonly IMediator _mediator;

        public ParseWebSiteRequestHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<PageResult> Handle(ParseWebSiteRequest request, CancellationToken cancellationToken)
        {
            var download = new WebSiteDownloadRequest(request.Url);
            var response = await _mediator.Send(download, cancellationToken);

            var validateResponse = new ValidateResponseRequest(response, request.Options);
            var isValid = await _mediator.Send(validateResponse, cancellationToken);

            if (!isValid) return new PageResult(request.Url.ToString());

            var parseHtml = new ParseHtmlRequest(await response.Content.ReadAsStringAsync());
            var parsed = await _mediator.Send(parseHtml, cancellationToken);

            var getLinks = new GetLinksRequest(parsed, request.Options.ParsingRules);
            var links = (await _mediator.Send(getLinks, cancellationToken)).ToList();

            var filterLinksRequest = new FilterLinksByHostRequest(links, request.Url);
            var filteredLinks = await _mediator.Send(filterLinksRequest, cancellationToken);

            var children = --request.MaxDepth <= 0 
                ? null
                : await Task.WhenAll(filteredLinks.Select(link =>
                    _mediator.Send(new ParseWebSiteRequest(link, request.MaxDepth, request.Options),
                        cancellationToken)));

            return new PageResult(request.Url.ToString(), links, children);
        }
    }
}