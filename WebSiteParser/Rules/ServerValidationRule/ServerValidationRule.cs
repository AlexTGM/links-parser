using System;
using System.Net.Http.Headers;
using WebSiteParser.Rules.Infrastructure;

namespace WebSiteParser.Rules.ServerValidationRule
{
    public class ServerValidationRule : IResponseHeaderValidationRule
    {
        private readonly Func<object[], ServerValidationRuleArgs> _argsFactory;

        public ServerValidationRule(IValidationRuleArgsParser<ServerValidationRuleArgs> argsParser)
        {
            _argsFactory = argsParser.Parse;
        }

        public string RuleName => nameof(ServerValidationRule);
        public object[] Args { get; set; }

        public bool IsApplicable(HttpResponseHeaders headers)
        {
            var parsedArgs = _argsFactory(Args);

            return headers.Server.ToString() == parsedArgs.PreferredServer;
        }
    }
}