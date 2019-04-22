using System;
using System.Collections.Generic;
using MediatR;

namespace WebSiteParser.Requests
{
    public class FilterLinksByHostRequest : IRequest<IEnumerable<Uri>>
    {
        public FilterLinksByHostRequest(IEnumerable<string> links, Uri url)
        {
            Links = links;
            Url = url;
        }

        public IEnumerable<string> Links { get; set; }
        public Uri Url { get; set; }
    }
}