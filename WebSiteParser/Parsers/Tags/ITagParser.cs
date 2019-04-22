using System.Collections.Generic;
using AngleSharp.Html.Dom;

namespace WebSiteParser.Parsers.Tags
{
    public interface ITagParser
    {
        string TagName { get; }

        IEnumerable<string> ParseTags(IHtmlDocument htmlDocument);
    }
}