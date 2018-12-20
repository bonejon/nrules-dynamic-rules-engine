using System;
using System.Collections.Generic;
using NRulesDynamicRulesEngine.Models;

namespace NRulesDynamicRulesEngine.Repository
{
    public sealed class RuleRepository
    {
        public IEnumerable<Rule> GetRules()
        {
            var rules = new List<Rule>
            {
                new Rule
                {
                    Id = Guid.NewGuid(),
                    Name = "Deactive users who haven't visited the site",
                    Criteria = new List<RuleCriteria>
                    {
                        new RuleCriteria { Id = Guid.NewGuid(), PropertyName = "DateOfLastVisit", Operation = "Equals", Value = null }
                    },
                    Actions = new List<RuleAction>
                    {
                        new RuleAction { Id = Guid.NewGuid(), PropertyName = "IsActive", Value = "false" }
                    }
                }
            };

            return rules;
        }
    }
}
