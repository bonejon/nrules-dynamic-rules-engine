using System.Collections.Generic;

namespace NRulesDynamicRulesEngine.Models
{
    public sealed class Rule : BaseEntity
    {
        public string Name { get; set; }

        public IEnumerable<RuleCriteria> Criteria { get; set; }

        public IEnumerable<RuleAction> Actions { get; set; }
    }
}
