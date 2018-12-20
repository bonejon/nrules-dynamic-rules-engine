using System;
using System.Collections.Generic;
using System.Linq;
using NRules;
using NRulesDynamicRulesEngine.Models;

namespace NRulesDynamicRulesEngine
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var visitors = new List<SiteVisitor>
            {
                new SiteVisitor { Id = Guid.NewGuid(), UserName = "FredBloggs", DateOfLastVisit = DateTime.MaxValue.AddDays(-5), DurationOfLastVisit = 3500, GeoIpCountryName = "UK", IpAddress = "10.1.2.3", IsActive = true },
                new SiteVisitor { Id = Guid.NewGuid(), UserName = "SteveJones", DateOfLastVisit = DateTime.MaxValue.AddDays(-5), DurationOfLastVisit = 3500, GeoIpCountryName = "UK", IpAddress = "10.1.2.3", IsActive = true },
                new SiteVisitor { Id = Guid.NewGuid(), UserName = "AnneWood", IsActive = true },
                new SiteVisitor { Id = Guid.NewGuid(), UserName = "JaneDoe", DateOfLastVisit = DateTime.MaxValue.AddDays(-1), DurationOfLastVisit = 13500, GeoIpCountryName = "US", IpAddress = "66.1.2.3", IsActive = true },
                new SiteVisitor { Id = Guid.NewGuid(), UserName = "ArnoldSchwarzenegger", IsActive = true }
            };

            // load the rules from the data store and buld NRules implementations
            var ruleBuilder = new DynamicRuleBuilder();
            ruleBuilder.GetRuleSets();

            // compile the NRules implementations and create a rules execution session
            var sessionFactory = ruleBuilder.Compile();
            var session = sessionFactory.CreateSession();

            // add the visitor data to the session
            session.InsertAll(visitors);

            // fire the rules
            session.Fire();

            // validate the rule output
            if (visitors.Count(v => v.IsActive) != 3)
            {
                Console.WriteLine("There where more than 3 visitors left active");
            }
            else
            {
                Console.WriteLine("There where 3 active visitors left. This is correct..");
            }

            Console.ReadLine();
        }
    }
}
