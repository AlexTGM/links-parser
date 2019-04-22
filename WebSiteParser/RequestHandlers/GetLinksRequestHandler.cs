using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebSiteParser.Requests;

namespace WebSiteParser.RequestHandlers
{
    public class GetLinksRequestHandler : IRequestHandler<GetLinksRequest, IEnumerable<string>>
    {
        private readonly IMediator _mediator;

        public GetLinksRequestHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IEnumerable<string>> Handle(GetLinksRequest request, CancellationToken cancellationToken)
        {
            var getParsingOptionsRequest = new GetParsingOptionsRequest(request.TagOptions);
            var parsingRules = await _mediator.Send(getParsingOptionsRequest, cancellationToken);

            var parseTagsRequest = new ParseTagsRequest(parsingRules.TagsParsers, request.HtmlDocument);
            var links = await _mediator.Send(parseTagsRequest, cancellationToken);

            var filterLinksRequest = new FilterLinksRequest(links, parsingRules);
            var filteredLinks = (await _mediator.Send(filterLinksRequest, cancellationToken)).ToList();

            return filteredLinks;
        }
    }
}