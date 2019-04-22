using System.Collections.Generic;
using System.Linq;
using WebSiteParser.Requests;

namespace WebSiteParser.RequestHandlers
{
    public class ExcludeLinksFilter : ILinksListFilter
    {
        public IEnumerable<string> ApplyFilter(IEnumerable<string> data, ParsingOptions parsingOptions)
        {
            return data.Where(link => parsingOptions.ExcludeWords.All(word => !link.Contains(word)));
        }
    }
}