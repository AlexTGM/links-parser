namespace WebSiteParser.Rules.Infrastructure
{
    public interface IValidationRuleArgsParser<out T>
        where T : IValidationRuleArgs
    {
        T Parse(object[] args);
    }
}