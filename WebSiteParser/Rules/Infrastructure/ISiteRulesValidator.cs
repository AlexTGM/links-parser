using System.Net.Http;

namespace WebSiteParser.Rules.Infrastructure
{
    public interface ISiteRulesValidator
    {
        bool Validate(HttpResponseMessage response, SiteValidationRules parsingRules);
    }
}