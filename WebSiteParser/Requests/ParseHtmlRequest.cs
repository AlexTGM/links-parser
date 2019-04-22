using AngleSharp.Html.Dom;
using MediatR;

namespace WebSiteParser.Requests
{
    public class ParseHtmlRequest : IRequest<IHtmlDocument>
    {
        public ParseHtmlRequest(string document)
        {
            Document = document;
        }

        public string Document { get; set; }
    }
}