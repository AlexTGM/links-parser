using System;
using MediatR;
using WebSiteParser.Controllers.Responses;

namespace WebSiteParser.Requests
{
    public class ParseWebSiteRequest : IRequest<PageResult>
    {
        public ParseWebSiteRequest(Uri url, int maxDepth, Options options)
        {
            Url = url;
            MaxDepth = maxDepth;
            Options = options;
        }

        public Uri Url { get; set; }
        public int MaxDepth { get; set; }

        public Options Options { get; set; }
    }
}