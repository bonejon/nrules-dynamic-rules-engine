using System;

namespace NRulesDynamicRulesEngine.Models
{
    public abstract class BaseEntity
    {
        public Guid? Id { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string LastUpdatedBy { get; set; }

        public DateTime LastUpdatedOn { get; set; }
    }
}
