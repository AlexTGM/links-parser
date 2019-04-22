using System;
using System.Collections.Generic;
using System.Linq;
using WebSiteParser.Rules.Infrastructure;

namespace WebSiteParser.Parsers.Options
{
    public class OptionsParser : IOptionsParser
    {
        public IReadOnlyList<RuleOptions> Parse(string input)
        {
            if (!input.Contains(':'))
                throw new ArgumentException("Input string is invalid", nameof(input));

            return input.Split(';').Select(rule =>
            {
                var options = rule.Split(':');
                var ruleName = options[0];

                var args = options[1] == ""
                    ? new object[0]
                    : options[1].Split(',');

                return new RuleOptions(ruleName, args);
            }).ToList();
        }
    }
}