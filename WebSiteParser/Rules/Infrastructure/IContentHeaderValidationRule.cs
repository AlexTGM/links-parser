using System.Net.Http.Headers;

namespace WebSiteParser.Rules.Infrastructure
{
    public interface IContentHeaderValidationRule : IValidationRule
    {
        bool IsApplicable(HttpContentHeaders headers);
    }
}