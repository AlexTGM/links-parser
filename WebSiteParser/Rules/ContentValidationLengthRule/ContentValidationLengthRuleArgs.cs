using WebSiteParser.Rules.Infrastructure;

namespace WebSiteParser.Rules.ContentValidationLengthRule
{
    public class ContentValidationLengthRuleArgs
        : IValidationRuleArgs
    {
        public long MaximumContentLength;
        public long MinimumContentLength;

        public ContentValidationLengthRuleArgs(long minimumContentLength, long maximumContentLength)
        {
            MinimumContentLength = minimumContentLength;
            MaximumContentLength = maximumContentLength;
        }
    }
}