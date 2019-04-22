using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using MediatR;
using WebSiteParser.Requests;

namespace WebSiteParser.RequestHandlers
{
    public class ParseHtmlRequestHandler : IRequestHandler<ParseHtmlRequest, IHtmlDocument>
    {
        private readonly IHtmlParser _htmlParser;

        public ParseHtmlRequestHandler(IHtmlParser htmlParser)
        {
            _htmlParser = htmlParser;
        }

        public async Task<IHtmlDocument> Handle(ParseHtmlRequest request, CancellationToken cancellationToken)
        {
            return await _htmlParser.ParseDocumentAsync(request.Document, cancellationToken);
        }
    }
}