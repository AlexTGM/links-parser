using System.Collections.Generic;
using System.Linq;

namespace WebSiteParser.Rules.Infrastructure
{
    public class ValidationRuleFactory : IValidationRuleFactory
    {
        private readonly IEnumerable<IValidationRule> _validationRules;

        public ValidationRuleFactory(IEnumerable<IValidationRule> validationRules)
        {
            _validationRules = validationRules;
        }

        public IValidationRule Create(string name, object[] args)
        {
            var rule = _validationRules.Single(r => r.RuleName == name);

            rule.Args = args;

            return rule;
        }
    }
}