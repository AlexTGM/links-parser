using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebSiteParser.Requests;

namespace WebSiteParser.RequestHandlers
{
    public class FilterLinksByHostRequestHandler : IRequestHandler<FilterLinksByHostRequest, IEnumerable<Uri>>
    {
        public Task<IEnumerable<Uri>> Handle(FilterLinksByHostRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(request.Links.Where(link => link.StartsWith("about:///")).
                Select(link => new Uri(request.Url, link.Replace("about://", ""))).Concat(request.Links.
                    Where(link => link.StartsWith("http")).Select(link => new Uri(link)).
                    Where(link => link.Host == request.Url.Host)));
        }
    }
}