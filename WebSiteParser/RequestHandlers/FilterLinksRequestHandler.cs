using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebSiteParser.Requests;

namespace WebSiteParser.RequestHandlers
{
    public class FilterLinksRequestHandler : IRequestHandler<FilterLinksRequest, IEnumerable<string>>
    {
        private readonly IEnumerable<ILinksListFilter> _linksListFilters;

        public FilterLinksRequestHandler(IEnumerable<ILinksListFilter> linksListFilters)
        {
            _linksListFilters = linksListFilters;
        }

        public async Task<IEnumerable<string>> Handle(FilterLinksRequest request, CancellationToken cancellationToken)
        {
            return _linksListFilters.Aggregate(request.Links, (current, linksListFilter) => linksListFilter.ApplyFilter(current, request.ParsingOptions));
        }
    }
}