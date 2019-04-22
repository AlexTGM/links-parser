using WebSiteParser.Rules.Infrastructure;

namespace WebSiteParser.Rules.ServerValidationRule
{
    public class ServerValidationRuleArgs
        : IValidationRuleArgs
    {
        public ServerValidationRuleArgs(string preferredServer)
        {
            PreferredServer = preferredServer;
        }

        public string PreferredServer { get; }
    }
}