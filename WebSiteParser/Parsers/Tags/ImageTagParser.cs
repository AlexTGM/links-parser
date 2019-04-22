using System.Collections.Generic;
using System.Linq;
using AngleSharp.Html.Dom;

namespace WebSiteParser.Parsers.Tags
{
    public class ImageTagParser : ITagParser
    {
        public string TagName => "img";

        public IEnumerable<string> ParseTags(IHtmlDocument htmlDocument)
        {
            return htmlDocument.QuerySelectorAll(TagName)
                .Cast<IHtmlImageElement>()
                .Select(selector => selector.Source).Distinct().ToList();
        }
    }
}