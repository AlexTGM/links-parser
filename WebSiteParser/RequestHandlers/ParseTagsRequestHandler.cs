using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebSiteParser.Requests;

namespace WebSiteParser.RequestHandlers
{
    public class ParseTagsRequestHandler : IRequestHandler<ParseTagsRequest, IEnumerable<string>>
    {
        public async Task<IEnumerable<string>> Handle(ParseTagsRequest request, CancellationToken cancellationToken)
        {
            var links = new List<string>();

            links = request.TagParsers.Aggregate(links,
                (current, parser) => current.Concat(parser.ParseTags(request.HtmlDocument)).ToList());

            return links;
        }
    }
}