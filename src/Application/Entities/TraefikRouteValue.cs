using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Entities
{
    public class TraefikRouteValue
    {
        public TraefikRouteValue()
        {

        }

        public TraefikRouteValue(string clusterId, string ruleId, string key, string value)
        {
            ClusterId = clusterId;
            RuleId = ruleId;
            Key = key;
            Value = value;
        }

        public TraefikRouteValue(string clusterId, string ruleId, string domain, string key, string value) : this(clusterId, ruleId, key, value)
        {
            Domain = domain;
        }

        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        [Required]
        public string ClusterId { get; set; }

        public string RuleId { get; set; }

        public string Domain { get; set; }

        [Required]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
