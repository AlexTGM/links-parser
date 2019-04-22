namespace WebSiteParser.Rules.Infrastructure
{
    public interface IValidationRule
    {
        string RuleName { get; }
        object[] Args { get; set; }
    }
}