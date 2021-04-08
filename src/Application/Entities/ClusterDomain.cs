using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Entities
{
    public class ClusterDomain
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        public string Name { get; set; }

        public string Value { get; set; }

        [Required]
        public string RootDomainId { get; set; }

        [Required]
        public string ClusterId { get; set; }

        [Required]
        public string Resolver { get; set; }

        public string FullChain { get; set; }

        public string Protocol { get; set; }

        public string PrivateKey { get; set; }

        [Required]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public DateTime? DeleteAt { get; set; }
    }
}
