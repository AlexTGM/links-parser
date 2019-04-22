using System;
using System.Collections.Generic;
using AngleSharp.Html.Dom;
using MediatR;

namespace WebSiteParser.Requests
{
    public class GetLinksRequest : IRequest<IEnumerable<string>>
    {
        public GetLinksRequest(IHtmlDocument htmlDocument, string tagOptions)
        {
            HtmlDocument = htmlDocument;
            TagOptions = tagOptions;
        }

        public IHtmlDocument HtmlDocument { get; set; }
        public string TagOptions { get; set; }
    }
}