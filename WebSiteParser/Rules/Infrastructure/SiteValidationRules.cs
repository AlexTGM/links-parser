using System.Collections.Generic;

namespace WebSiteParser.Rules.Infrastructure
{
    public class SiteValidationRules
    {
        public IEnumerable<IContentHeaderValidationRule> ContentRulesList { get; set; }
        public IEnumerable<IResponseHeaderValidationRule> ResponseValidationRules { get; set; }
    }
}