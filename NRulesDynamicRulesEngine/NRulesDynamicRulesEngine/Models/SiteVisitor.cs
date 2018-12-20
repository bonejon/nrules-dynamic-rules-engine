using System;

namespace NRulesDynamicRulesEngine.Models
{
    public sealed class SiteVisitor : BaseEntity
    {
        public string UserName { get; set; }

        public string IpAddress { get; set; }

        public DateTime? DateOfLastVisit { get; set; }

        public int? DurationOfLastVisit { get; set; }

        public string GeoIpCountryName { get; set; }

        public bool IsActive { get; set; }
    }
}
