using System.Collections.Generic;
using WebSiteParser.Rules.Infrastructure;

namespace WebSiteParser.Parsers.Options
{
    public interface IOptionsParser
    {
        IReadOnlyList<RuleOptions> Parse(string input);
    }
}