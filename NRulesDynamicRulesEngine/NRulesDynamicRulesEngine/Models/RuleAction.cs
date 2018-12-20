namespace NRulesDynamicRulesEngine.Models
{
    public sealed class RuleAction : BaseEntity
    {
        public string PropertyName { get; set; }

        public string Value { get; set; }
    }
}
