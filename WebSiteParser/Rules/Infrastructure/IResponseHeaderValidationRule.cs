using System.Net.Http.Headers;

namespace WebSiteParser.Rules.Infrastructure
{
    public interface IResponseHeaderValidationRule : IValidationRule
    {
        bool IsApplicable(HttpResponseHeaders headers);
    }
}