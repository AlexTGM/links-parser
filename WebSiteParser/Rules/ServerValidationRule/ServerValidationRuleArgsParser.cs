using WebSiteParser.Rules.Infrastructure;

namespace WebSiteParser.Rules.ServerValidationRule
{
    public class ServerValidationRuleArgsParser
        : IValidationRuleArgsParser<ServerValidationRuleArgs>
    {
        public ServerValidationRuleArgs Parse(object[] args)
        {
            var preferredServer = args[0] as string;

            return new ServerValidationRuleArgs(preferredServer);
        }
    }
}