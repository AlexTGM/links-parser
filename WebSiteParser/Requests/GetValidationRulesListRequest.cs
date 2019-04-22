using MediatR;
using WebSiteParser.Rules.Infrastructure;

namespace WebSiteParser.Requests
{
    public class GetValidationRulesListRequest : IRequest<SiteValidationRules>
    {
        public GetValidationRulesListRequest(string contentRules, string responseRules)
        {
            ContentRules = contentRules;
            ResponseRules = responseRules;
        }

        public string ContentRules { get; set; }
        public string ResponseRules { get; set; }
    }
}