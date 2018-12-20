namespace NRulesDynamicRulesEngine.Models
{
    public sealed class RuleCriteria : BaseEntity
    {
        public string PropertyName { get; set; }

        public string Operation { get; set; }

        public string Value { get; set; }
    }
}
