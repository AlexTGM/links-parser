using System.Collections.Generic;
using System.Linq;
using AngleSharp.Html.Dom;

namespace WebSiteParser.Parsers.Tags
{
    public class AnchorTagParser : ITagParser
    {
        public string TagName => "a";

        public IEnumerable<string> ParseTags(IHtmlDocument htmlDocument)
        {
            return htmlDocument.QuerySelectorAll(TagName)
                .Cast<IHtmlAnchorElement>()
                .Select(selector => selector.Href).Distinct().ToList();
        }
    }
}