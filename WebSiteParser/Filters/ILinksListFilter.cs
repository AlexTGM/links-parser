using System.Collections.Generic;
using WebSiteParser.Requests;

namespace WebSiteParser.RequestHandlers
{
    public interface ILinksListFilter
    {
        IEnumerable<string> ApplyFilter(IEnumerable<string> data, ParsingOptions parsingOptions);
    }
}