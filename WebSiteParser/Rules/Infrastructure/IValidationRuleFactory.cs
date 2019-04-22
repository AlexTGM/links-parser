namespace WebSiteParser.Rules.Infrastructure
{
    public interface IValidationRuleFactory
    {
        IValidationRule Create(string name, object[] args);
    }
}