using System;
using System.Net.Http.Headers;
using WebSiteParser.Rules.Infrastructure;

namespace WebSiteParser.Rules.ContentValidationLengthRule
{
    public class ContentValidationLengthRule : IContentHeaderValidationRule
    {
        private readonly Func<object[], ContentValidationLengthRuleArgs> _argsFactory;

        public ContentValidationLengthRule(IValidationRuleArgsParser<ContentValidationLengthRuleArgs> argsParser)
        {
            _argsFactory = argsParser.Parse;
        }

        public string RuleName => nameof(ContentValidationLengthRule);
        public object[] Args { get; set; }

        public bool IsApplicable(HttpContentHeaders headers)
        {
            var parsedArgs = _argsFactory(Args);

            return headers.ContentLength >= parsedArgs.MinimumContentLength
                   && headers.ContentLength <= parsedArgs.MaximumContentLength;
        }
    }
}