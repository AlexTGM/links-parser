using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebSiteParser.Parsers;
using WebSiteParser.Parsers.Options;
using WebSiteParser.Requests;
using WebSiteParser.Rules.Infrastructure;

namespace WebSiteParser.RequestHandlers
{
    public class GetValidationRulesListRequestHandler 
        : IRequestHandler<GetValidationRulesListRequest, SiteValidationRules>
    {
        private readonly IOptionsParser _optionsParser;
        private readonly IValidationRuleFactory _validationRuleFactory;

        public GetValidationRulesListRequestHandler(IOptionsParser optionsParser,
            IValidationRuleFactory validationRuleFactory)
        {
            _optionsParser = optionsParser;
            _validationRuleFactory = validationRuleFactory;
        }

        public Task<SiteValidationRules> Handle(GetValidationRulesListRequest request,
            CancellationToken cancellationToken)
        {
            var contentRulesList = request.ContentRules != null ? _optionsParser.Parse(request.ContentRules)
                .Select(options => _validationRuleFactory.Create(options.RuleName, options.Args))
                .Cast<IContentHeaderValidationRule>() : Enumerable.Empty<IContentHeaderValidationRule>();

            var responseRulesList = request.ResponseRules != null ? _optionsParser.Parse(request.ResponseRules)
                .Select(options => _validationRuleFactory.Create(options.RuleName, options.Args))
                .Cast<IResponseHeaderValidationRule>() : Enumerable.Empty<IResponseHeaderValidationRule>();

            return Task.FromResult(new SiteValidationRules
            {
                ContentRulesList = contentRulesList,
                ResponseValidationRules = responseRulesList
            });
        }
    }
}