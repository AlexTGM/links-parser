using System.Linq;
using WebSiteParser.Rules.Infrastructure;

namespace WebSiteParser.Rules.ContentValidationLengthRule
{
    public class ContentValidationLengthRuleArgsParser
        : IValidationRuleArgsParser<ContentValidationLengthRuleArgs>
    {
        public ContentValidationLengthRuleArgs Parse(object[] args)
        {
            var minimumContentLength = 0L;
            var maximumContentLength = long.MaxValue;

            if (args.Any())
                long.TryParse(args[0] as string, out minimumContentLength);

            if (args.Length > 1)
                long.TryParse(args[1] as string, out maximumContentLength);

            if (minimumContentLength < 0)
                minimumContentLength = 0L;

            if (maximumContentLength < 0)
                maximumContentLength = long.MaxValue;

            if (maximumContentLength < minimumContentLength)
            {
                minimumContentLength = 0L;
                maximumContentLength = long.MaxValue;
            }

            return new ContentValidationLengthRuleArgs(minimumContentLength, maximumContentLength);
        }
    }
}