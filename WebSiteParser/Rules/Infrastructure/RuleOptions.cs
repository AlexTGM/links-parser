namespace WebSiteParser.Rules.Infrastructure
{
    public class RuleOptions
    {
        public RuleOptions(string ruleName, object[] args)
        {
            RuleName = ruleName;
            Args = args;
        }

        public string RuleName { get; set; }
        public object[] Args { get; set; }
    }
}