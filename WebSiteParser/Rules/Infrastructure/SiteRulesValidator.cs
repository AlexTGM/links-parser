using System.Linq;
using System.Net.Http;

namespace WebSiteParser.Rules.Infrastructure
{
    public class SiteRulesValidator : ISiteRulesValidator
    {
        public bool Validate(HttpResponseMessage response, SiteValidationRules parsingRules)
        {
            return parsingRules.ContentRulesList
                       .All(rule => rule.IsApplicable(response.Content.Headers))
                   && parsingRules.ResponseValidationRules
                       .All(rule => rule.IsApplicable(response.Headers));
        }
    }
}