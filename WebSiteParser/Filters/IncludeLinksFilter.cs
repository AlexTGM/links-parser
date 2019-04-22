using System.Collections.Generic;
using System.Linq;
using WebSiteParser.Requests;

namespace WebSiteParser.RequestHandlers
{
    public class IncludeLinksFilter : ILinksListFilter
    {
        public IEnumerable<string> ApplyFilter(IEnumerable<string> data, ParsingOptions parsingOptions)
        {
            return data.Where(link => parsingOptions.IncludeWords.All(link.Contains));
        }
    }
}