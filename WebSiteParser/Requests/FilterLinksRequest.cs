using System.Collections.Generic;
using MediatR;

namespace WebSiteParser.Requests
{
    public class FilterLinksRequest : IRequest<IEnumerable<string>>
    {
        public IEnumerable<string> Links { get; set; }
        public ParsingOptions ParsingOptions { get; set; }

        public FilterLinksRequest(IEnumerable<string> links, ParsingOptions parsingOptions)
        {
            Links = links;
            ParsingOptions = parsingOptions;
        }
    }
}