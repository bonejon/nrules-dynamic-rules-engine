using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FastMember;
using NRules.RuleModel;
using NRules.RuleModel.Builders;
using NRulesDynamicRulesEngine.Models;
using NRulesDynamicRulesEngine.Repository;

namespace NRulesDynamicRulesEngine
{
    public sealed class DynamicRuleBuilder : IRuleRepository
    {
        private readonly IRuleSet _ruleSet = new RuleSet("DefaultRuleSet");
        private readonly TypeAccessor _siteVisitorAccessor = TypeAccessor.Create(typeof(SiteVisitor));

        public IEnumerable<IRuleSet> GetRuleSets()
        {
            var rules = new RuleRepository().GetRules();

            _ruleSet.Add(rules.Select(BuildRule).ToArray());

            return new [] { _ruleSet };
        }

        private IRuleDefinition BuildRule(Rule definition)
        {
            var ruleBuilder = new RuleBuilder();
            ruleBuilder.Name(definition.Name);

            var ruleProperties = new List<RuleProperty>
            {
                new RuleProperty("Definition", definition),
            };

            ruleBuilder.Properties(ruleProperties);

            var patternBuilder = ruleBuilder.LeftHandSide().Pattern(typeof(SiteVisitor), "siteVisitor");

            foreach (var criterion in definition.Criteria)
            {
                var expression = default(Expression<Func<SiteVisitor, bool>>);

                if (criterion.Operation.Equals("Equals", StringComparison.InvariantCultureIgnoreCase))
                {
                    expression = siteVisitor => _siteVisitorAccessor[siteVisitor, criterion.PropertyName] == ChangeType(criterion.Value, criterion.PropertyName);
                }

                if (criterion.Operation.Equals("NotEquals", StringComparison.InvariantCultureIgnoreCase))
                {
                    expression = siteVisitor => _siteVisitorAccessor[siteVisitor, criterion.PropertyName] != ChangeType(criterion.Value, criterion.PropertyName);
                }

                if (expression != null)
                {
                    patternBuilder.Condition(expression);
                }
            }

            Expression<Action<IContext, SiteVisitor>> action = (ctx, siteVisitor) => SetMemberDetails(ctx, siteVisitor);
            ruleBuilder.RightHandSide().Action(action);

            return ruleBuilder.Build();
        }

        private object ChangeType(object value, string propertyName)
        {
            var type = _siteVisitorAccessor.GetMembers()
                .Single(p => p.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase)).Type;

            if (Nullable.GetUnderlyingType(type) != null && value == null)
            {
                return null;
            }

            return Convert.ChangeType(value, type);
        }

        private void SetMemberDetails(IContext context, SiteVisitor siteVisitor)
        {
            if (context.Rule.Properties["Definition"] is Rule ruleDefinition)
            {
                foreach (var action in ruleDefinition.Actions)
                {
                    Console.WriteLine($"Updating visitor {siteVisitor.UserName}, setting {action.PropertyName} to {action.Value}");

                    _siteVisitorAccessor[siteVisitor, action.PropertyName] = ChangeType(action.Value, action.PropertyName);
                }
            }
        }
    }
}
