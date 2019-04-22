using System.Collections.Generic;
using AngleSharp.Html.Dom;
using MediatR;
using WebSiteParser.Parsers.Tags;

namespace WebSiteParser.Requests
{
    public class ParseTagsRequest : IRequest<IEnumerable<string>>
    {
        public ParseTagsRequest(IEnumerable<ITagParser> tagParsers, IHtmlDocument htmlDocument)
        {
            TagParsers = tagParsers;
            HtmlDocument = htmlDocument;
        }

        public IEnumerable<ITagParser> TagParsers { get; set; }
        public IHtmlDocument HtmlDocument { get; set; }
    }
}